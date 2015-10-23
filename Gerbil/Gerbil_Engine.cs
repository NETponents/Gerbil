using System;
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
            public OSResult guessOS()
            {
                OSResult result;
                // Initialize objects
                NeuralNetwork.Network net = new NeuralNetwork.Network();

                // Fake data for now
                result = new OSResult("Unknown", 0.0f);
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
                public void addConnection(ref Connection input)
                {
                    input.Fired += new Connection.ConnectionFiredHandler(Fire);
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
                    e.tag = new Random().Next(10000, 99999).ToString();
                    base.Fire(this, e);
                }
            }
            class OutputNode : Node
            {
                private float result;

                public OutputNode(string nName, ref Dictionary<string, Connection> input, string connectorSelector)
                    : base(nName, ref input, connectorSelector)
                {
                    result = 0;
                }
                public override void Fire(object sender, NetPathEventArgs e)
                {
                    result *= (e.value + 1);
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
                private Dictionary<string,Node> nodes;
                private Dictionary<string, InputNode> inputs;
                private Dictionary<string, OutputNode> outputs;
                private Dictionary<string, Connection> connectors;

                public Network()
                {
                    nodes = new Dictionary<string, Node>();
                    inputs = new Dictionary<string, InputNode>();
                    outputs = new Dictionary<string, OutputNode>();
                }
                public void addNode(string nName, string cName, float weight, string inNode)
                {
                    addConnector(cName, weight, inNode);
                    nodes.Add(nName, new Node(nName, ref connectors, cName));
                }
                public void addConnector(string cName, float weight, string inNode)
                {
                    if (inputs.ContainsKey(inNode))
                    {
                        connectors.Add(cName, new Connection(weight, ref inputs, inNode));
                    }
                    else if (nodes.ContainsKey(inNode))
                    {
                        connectors.Add(cName, new Connection(weight, ref nodes, inNode));
                    }
                    else
                    {
                        throw new NodeNotFoundException();
                    }
                }
                public void addOutput(string oName, string cName, float weight, string inNode)
                {
                    addConnector(cName, weight, inNode);
                    outputs.Add(oName, new OutputNode(oName, ref connectors, cName));
                }
                public void addInput(string name)
                {
                    inputs.Add(name, new InputNode(name));
                }
                public Dictionary<string, float> getResults()
                {
                    Dictionary<string, float> results = new Dictionary<string, float>();
                    foreach(KeyValuePair<string, OutputNode> i in outputs)
                    {
                        results.Add(i.Key, i.Value.getResult());
                    }
                    return results;
                }
                public void fireInput(string fireNode)
                {
                    inputs[fireNode].Fire();
                }
            }
            class NodeNotFoundException : Exception
            {

            }
        }
    }
}
