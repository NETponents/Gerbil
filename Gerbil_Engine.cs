using System;
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
            public static OSResult guessOS(string[] foundServices)
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
                        // Serive does not exist, since we are not in training mode, ignore.
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
        namespace NeuralNetwork
        {
            class Node
            {
                public delegate void NodeFiredHandler(object sender, NetPathEventArgs e);
                public event NodeFiredHandler Fired;

                private string nodeName;

                public Node()
                {
                    nodeName = "";
                }
                public Node(string nName)
                {
                    nodeName = nName;
                }
                public Node(string nName, ref Dictionary<string, Connection> input, string connectorSelector)
                {
                    nodeName = nName;
                    input[connectorSelector].Fired += new Connection.ConnectionFiredHandler(Fire);
                }
                public virtual void Fire(object sender, NetPathEventArgs e)
                {
                    Fired(sender, e);
                }
                public string getName()
                {
                    return nodeName;
                }
                public void addConnection(ref Dictionary<string, Connection> input, string connectorSelector)
                {
                    input[connectorSelector].Fired += new Connection.ConnectionFiredHandler(Fire);
                }
            }
            class InputNode : Node
            {
                public InputNode()
                    : base()
                {

                }
                public InputNode(string nName)
                    : base(nName)
                {

                }
                public void Fire()
                {
                    NetPathEventArgs e = new NetPathEventArgs();
                    e.value = 1.0f;
                    e.tag = new Random().Next(1000000000, 9999999999).ToString();
                    base.Fire(this, e);
                }
            }
            class OutputNode : Node
            {
                private float result;

                public OutputNode(string nName, ref Dictionary<string, Connection> input, string connectorSelector)
                    : base(nName, ref input, connectorSelector)
                {
                    result = 1.0f;
                }
                public override void Fire(object sender, NetPathEventArgs e)
                {
                    result += (e.value + 1);
                }
                public float getResult()
                {
                    return result;
                }
            }
            class Connection
            {
                public delegate void ConnectionFiredHandler(object sender, NetPathEventArgs e);
                public event ConnectionFiredHandler Fired;

                public float Weight
                {
                    get
                    {
                        return weight;
                    }
                    set
                    {
                        if(verifyWeight(value))
                        {
                            weight = value;
                        }
                    }
                }
                private float weight;

                public Connection(float cWeight, ref Node input)
                {
                    input.Fired += new Node.NodeFiredHandler(Fire);
                    Weight = cWeight;
                }
                public Connection(float cWeight, ref Dictionary<string, Node> input, string nodeSelector)
                {
                    input[nodeSelector].Fired += new Node.NodeFiredHandler(Fire);
                    Weight = cWeight;
                }
                public Connection(float cWeight, ref Dictionary<string, InputNode> input, string nodeSelector)
                {
                    input[nodeSelector].Fired += new Node.NodeFiredHandler(Fire);
                    Weight = cWeight;
                }
                public void Fire(object sender, NetPathEventArgs e)
                {
                    e.value = e.value * (Weight + 1);
                    Fired(sender, e);
                }
                public static bool verifyWeight(float input)
                {
                    if(input.GetType() == typeof(float) && input >= 0.0f)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            class NetPathEventArgs : EventArgs
            {
                public float value { get; set; }
                public string tag { get; set; }
            }
            class Network
            {
                public static Random rd = new Random();

                private Dictionary<string,Node> nodes;
                private Dictionary<string, InputNode> inputs;
                private Dictionary<string, OutputNode> outputs;
                private Dictionary<string, Connection> connectors;

                public Network()
                {
                    nodes = new Dictionary<string, Node>();
                    inputs = new Dictionary<string, InputNode>();
                    outputs = new Dictionary<string, OutputNode>();
                    connectors = new Dictionary<string, Connection>();
                }
                public void addNode(string nName, string cName, float weight, string inNode)
                {
                    addConnector(cName, weight, inNode);
                    nodes.Add(nName, new Node(nName, ref connectors, cName));
                }
                public string addConnector(string cName, float weight, string inNode)
                {
                    string rcName = cName;
                    if(connectors.ContainsKey(cName))
                    {
                        rcName = rcName + rd.Next(100000, 999999).ToString();
                    }
                    if (inputs.ContainsKey(inNode))
                    {
                        connectors.Add(rcName, new Connection(weight, ref inputs, inNode));
                    }
                    else if (nodes.ContainsKey(inNode))
                    {
                        connectors.Add(rcName, new Connection(weight, ref nodes, inNode));
                    }
                    else
                    {
                        throw new NodeNotFoundException();
                    }
                    return rcName;
                }
                public void addOutput(string oName, string cName, float weight, string inNode)
                {
                    string rcName = addConnector(cName, weight, inNode);
                    if (!outputs.ContainsKey(oName))
                    {
                        outputs.Add(oName, new OutputNode(oName, ref connectors, rcName));
                    }
                    else
                    {
                        outputs[oName].addConnection(ref connectors, rcName);
                    }
                }
                public void addInput(string name)
                {
                    if(inputs.ContainsKey(name))
                    {
                        // Do nothing, input key already exists
                    }
                    else
                    {
                        inputs.Add(name, new InputNode(name));
                    }
                }
                public Dictionary<string, float> getResults()
                {
                    if (inputs.Count == 0 || outputs.Count == 0)
                    {
                        throw new EmptyNodeGroupException();
                    }
                    Dictionary<string, float> results = new Dictionary<string, float>();
                    foreach(KeyValuePair<string, OutputNode> i in outputs)
                    {
                        results.Add(i.Key, i.Value.getResult());
                    }
                    return results;
                }
                public void fireInput(string fireNode)
                {
                    if(inputs.Count == 0 || outputs.Count == 0)
                    {
                        throw new EmptyNodeGroupException();
                    }
                    if (inputs.ContainsKey(fireNode))
                    {
                        inputs[fireNode].Fire();
                    }
                    else
                    {
                        throw new NodeNotFoundException();
                    }
                }
            }
            class NodeNotFoundException : Exception
            {

            }
            class EmptyNodeGroupException : Exception
            {

            }
        }
        class FatalEngineException : Exception
        {

        }
    }
}
