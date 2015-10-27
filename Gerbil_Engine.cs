﻿using System;
using System.IO;
using System.Collections.Generic;
using Gerbil.Gerbil_DataService;
using Gerbil.Gerbil_DataService.Models.Devices;

namespace Gerbil
{
    namespace Gerbil_Engine
    {
        class GerbilRunner
        {
            private Device target;

            public GerbilRunner(Device dTarget)
            {
                target = dTarget;
            }
            public static OSResult guessOS(string[] foundServices, bool training)
            {
                OSResult result;
                // Initialize objects
                NeuralNetwork.Network net = new NeuralNetwork.Network();

                // Load in data to memory
                if (!File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "OSServiceTraining.ini")))
                {
                    return new OSResult("ERROR", 0.0f);
                }
                string[] trainingData = File.ReadAllLines(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore","OSServiceTraining.ini"));
                // Calculate weights
                PairCounter pc = new PairCounter();
                foreach(string i in trainingData)
                {
                    string sName = i.Split('=')[0];
                    string fOS = i.Split('=')[1];
                    pc.Add(new Pair(sName, fOS));
                }
                Dictionary<Pair, float> connectionWeights = getPercentagesFromPair(pc.getResults());
                // TODO: Train network
                foreach(KeyValuePair<Pair, float> i in connectionWeights)
                {
                    net.addInput(i.Key.item1);
                    net.addOutput(i.Key.item2, i.Key.item1 + "Connector", i.Value, i.Key.item1);
                }
                // Feed data into tranined neural network
                foreach(string i in foundServices)
                {
                    try
                    {
                        net.fireInput(i);
                    }
                    catch(NeuralNetwork.NodeNotFoundException e)
                    {
                        // Service does not exist, since we are not in training mode, ignore.
                        if(training)
                        {
                            //TODO: Prompt for training input
                            Gerbil_IO.Out.writeln("Unknown input service: " + i);
                        }
                        else
                        {
                            throw e;
                        }
                    }
                    catch
                    {
                        // A serious engine error occured. Throw fatal error.
                        throw new FatalEngineException();
                    }
                }
                // Get outputs
                Dictionary<string, float> results = net.getResults();
                string resultName = "Unknown";
                float resultCertainty = 0.0f;
                float maxCertainty = 0.0f;
                // Find most likely answer
                foreach(KeyValuePair<string, float> i in results)
                {
                    if(i.Value > resultCertainty)
                    {
                        resultName = i.Key;
                        resultCertainty = i.Value;
                        maxCertainty += i.Value;
                    }
                }
                resultCertainty = resultCertainty / maxCertainty;
                result = new OSResult(resultName, resultCertainty);
                return result;
            }
            private static Dictionary<Pair, float> getPercentagesFromPair(Dictionary<Pair, int> input)
            {
                int max = 0;
                foreach (KeyValuePair<Pair, int> i in input)
                {
                    max += i.Value;
                }
                Dictionary<Pair, float> result = new Dictionary<Pair, float>();
                foreach (KeyValuePair<Pair, int> i in input)
                {
                    result.Add(i.Key, (float)(i.Value / (float)max));
                }
                return result;
            }
        }
        struct OSResult
        {
            private string osName;
            private float certainty;

            public OSResult(string name, float ct)
            {
                osName = name;
                certainty = ct;
            }
            public string getName()
            {
                return osName;
            }
            public float getCertainty()
            {
                return certainty;
            }
        }
        class PairCounter
        {
            Dictionary<Pair, int> dataHolder;
            public PairCounter()
            {
                dataHolder = new Dictionary<Pair, int>();
            }
            public void Add(Pair addon)
            {
                if(dataHolder.ContainsKey(addon))
                {
                    dataHolder[addon] += 1;
                }
                else
                {
                    dataHolder.Add(addon, 1);
                }
            }
            public int getCount(Pair item)
            {
                if (dataHolder.ContainsKey(item))
                {
                    return dataHolder[item];
                }
                else
                {
                    return 0;
                }
            }
            public Dictionary<Pair, int> getResults()
            {
                return dataHolder;
            }
        }
        class Pair
        {
            public string item1 = "";
            public string item2 = "";
            public Pair(string a, string b)
            {
                item1 = a;
                item2 = b;
            }
        }
        class FatalEngineException : Exception
        {

        }
    }
}
