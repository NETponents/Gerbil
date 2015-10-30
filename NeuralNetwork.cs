using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerbil
{
    namespace NeuralNetwork
    {
        /// <summary>
        /// Base class for network nodes.
        /// </summary>
        class Node
        {
            public delegate void NodeFiredHandler(object sender, NetPathEventArgs e);
            public event NodeFiredHandler Fired;

            private string nodeName;

            /// <summary>
            /// Constructor method for Node class.
            /// </summary>
            public Node()
            {
                nodeName = "";
            }
            /// <summary>
            /// Constructor method for Node class
            /// </summary>
            /// <param name="nName">Name of node</param>
            public Node(string nName)
            {
                nodeName = nName;
            }
            /// <summary>
            /// Constructor method for Node class.
            /// </summary>
            /// <param name="nName">Name of node</param>
            /// <param name="input">Pointer to a list of active network connections</param>
            /// <param name="connectorSelector">Name of connector to trigger new node</param>
            public Node(string nName, ref Dictionary<string, Connection> input, string connectorSelector)
            {
                nodeName = nName;
                input[connectorSelector].Fired += new Connection.ConnectionFiredHandler(Fire);
            }
            /// <summary>
            /// Event handler for Node object.
            /// </summary>
            /// <param name="sender">Sender the fired the event (Type:InputNode)</param>
            /// <param name="e">EventArg carrying fire value</param>
            public virtual void Fire(object sender, NetPathEventArgs e)
            {
                Fired(sender, e);
            }
            /// <summary>
            /// Gets the name of the specified node
            /// </summary>
            /// <returns>Name of referenced node</returns>
            public string getName()
            {
                return nodeName;
            }
            /// <summary>
            /// Gets the name of the specified node
            /// </summary>
            /// <param name="nd">Node to examine</param>
            /// <returns>Name of referenced node</returns>
            public static string getName(Node nd)
            {
                return nd.getName();
            }
            /// <summary>
            /// Adds a connector fire event to the specified node
            /// </summary>
            /// <param name="input">Pointer to list of active connection list</param>
            /// <param name="connectorSelector">Name of connector to bind to Fired() event</param>
            public void addConnection(ref Dictionary<string, Connection> input, string connectorSelector)
            {
                input[connectorSelector].Fired += new Connection.ConnectionFiredHandler(Fire);
            }
        }
        /// <summary>
        /// Node that does not recieve input, but can be fired using an outside method call.
        /// </summary>
        class InputNode : Node
        {
            /// <summary>
            /// Constructor class for InputNode
            /// </summary>
            public InputNode()
                : base()
            {

            }
            /// <summary>
            /// Constructor class for InputNode
            /// </summary>
            /// <param name="nName">Name of node to create</param>
            public InputNode(string nName)
                : base(nName)
            {

            }
            /// <summary>
            /// Trigger to fire an event through the neural network.
            /// </summary>
            public void Fire()
            {
                NetPathEventArgs e = new NetPathEventArgs();
                e.value = 1.0f;
                e.tag = new Random().Next(100000000, 999999999).ToString();
                base.Fire(this, e);
            }
        }
        /// <summary>
        /// Node that stores its value and cannot be tagged as an input to another connection.
        /// </summary>
        class OutputNode : Node
        {
            private float result;

            /// <summary>
            /// Constructor for OutputNode class
            /// </summary>
            /// <param name="nName">Name of node to create</param>
            /// <param name="input">Pointer to list of active connections in the network</param>
            /// <param name="connectorSelector">Name of connection to bind to Fired() event</param>
            public OutputNode(string nName, ref Dictionary<string, Connection> input, string connectorSelector)
                : base(nName, ref input, connectorSelector)
            {
                result = 1.0f;
            }
            /// <summary>
            /// Event handler for Fired() event. Stores input value.
            /// </summary>
            /// <param name="sender">Event trigger</param>
            /// <param name="e">EventArg containing neural trigger value</param>
            public override void Fire(object sender, NetPathEventArgs e)
            {
                result += (e.value + 1);
            }
            /// <summary>
            /// Gets value of OutputNode
            /// </summary>
            /// <returns>Value of node</returns>
            public float getResult()
            {
                return result;
            }
        }
        /// <summary>
        /// Node that holds its value for checkpoint lookups
        /// </summary>
        class CheckpointNode : Node
        {
            private float result;
            
            /// <summary>
            /// Constructor for CheckpointNode class
            /// </summary>
            /// <param name="nName">Name of node</param>
            /// <param name="input">Pointer to list of active connections in network</param>
            /// <param name="connectorSelector">Name of connector to bind Fired() event to</param>
            public CheckpointNode(string nName, ref Dictionary<string, Connection> input, string connectorSelector)
                : base(nName, ref input, connectorSelector)
            {
                result = 1.0f;
            }
            /// <summary>
            /// Event handler for Connection Fired() event
            /// </summary>
            /// <param name="sender">InputNode event sender</param>
            /// <param name="e">Tag containing neural value</param>
            public override void Fire(object sender, NetPathEventArgs e)
            {
                result += (e.value + 1);
                base.Fire(sender, e);
            }
            /// <summary>
            /// Get checkpoint value of node
            /// </summary>
            /// <returns>Value of node</returns>
            public float getResult()
            {
                return result;
            }
        }
        /// <summary>
        /// Connector to bind two nodes together
        /// </summary>
        class Connection
        {
            public delegate void ConnectionFiredHandler(object sender, NetPathEventArgs e);
            public event ConnectionFiredHandler Fired;

            /// <summary>
            /// Holds weight to be applied to EventArg tag
            /// </summary>
            public float Weight
            {
                get
                {
                    return weight;
                }
                set
                {
                    if (verifyWeight(value))
                    {
                        weight = value;
                    }
                }
            }
            private float weight;

            /// <summary>
            /// Constructor for Connection class
            /// </summary>
            /// <param name="cWeight">Weight of connection</param>
            /// <param name="input">Node output Fire() event to bind to</param>
            public Connection(float cWeight, ref Node input)
            {
                input.Fired += new Node.NodeFiredHandler(Fire);
                Weight = cWeight;
            }
            /// <summary>
            /// Constructor for Connection class
            /// </summary>
            /// <param name="cWeight">Weight of connection</param>
            /// <param name="input">Pointer to list of active nodes</param>
            /// <param name="nodeSelector">Name of node to bind Fire() output event to</param>
            public Connection(float cWeight, ref Dictionary<string, Node> input, string nodeSelector)
            {
                input[nodeSelector].Fired += new Node.NodeFiredHandler(Fire);
                Weight = cWeight;
            }
            /// <summary>
            /// Constructor for Connection class
            /// </summary>
            /// <param name="cWeight">Weight of connection</param>
            /// <param name="input">Pointer to list of active nodes</param>
            /// <param name="nodeSelector">Name of InputNode to bind Fire() output event to</param>
            public Connection(float cWeight, ref Dictionary<string, InputNode> input, string nodeSelector)
            {
                input[nodeSelector].Fired += new Node.NodeFiredHandler(Fire);
                Weight = cWeight;
            }
            /// <summary>
            /// Event handler on node Fire()
            /// </summary>
            /// <param name="sender">InputNode sender</param>
            /// <param name="e">Tag containing neural value</param>
            public void Fire(object sender, NetPathEventArgs e)
            {
                e.value = e.value * (Weight + 1);
                Fired(sender, e);
            }
            /// <summary>
            /// Verifies input weight of a Connection
            /// </summary>
            /// <param name="input">Input weight</param>
            /// <returns>Weight is valid</returns>
            public static bool verifyWeight(float input)
            {
                if (input.GetType() == typeof(float) && input >= 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Neural fire tag class
        /// </summary>
        class NetPathEventArgs : EventArgs
        {
            public float value { get; set; }
            public string tag { get; set; }
        }
        /// <summary>
        /// Base network class
        /// </summary>
        class Network
        {
            public static Random rd = new Random();

            private Dictionary<string, Node> nodes;
            private Dictionary<string, InputNode> inputs;
            private Dictionary<string, OutputNode> outputs;
            private Dictionary<string, Connection> connectors;

            /// <summary>
            /// Constructor for Network class
            /// </summary>
            public Network()
            {
                nodes = new Dictionary<string, Node>();
                inputs = new Dictionary<string, InputNode>();
                outputs = new Dictionary<string, OutputNode>();
                connectors = new Dictionary<string, Connection>();
            }
            /// <summary>
            /// Adds a node to the network
            /// </summary>
            /// <param name="nName">Name of node to add</param>
            /// <param name="cName">Name of connector to connect to node</param>
            /// <param name="weight">Weight of connector</param>
            /// <param name="inNode">Node feeding into connector/node</param>
            public void addNode(string nName, string cName, float weight, string inNode)
            {
                string cName2 = addConnector(cName, weight, inNode);
                nodes.Add(nName, new Node(nName, ref connectors, cName2));
            }
            /// <summary>
            /// Adds a connector to the network
            /// </summary>
            /// <param name="cName">Name of connector to add</param>
            /// <param name="weight">Weight of connector</param>
            /// <param name="inNode">Node feeding into connector</param>
            /// <returns>Name of connector created</returns>
            public string addConnector(string cName, float weight, string inNode)
            {
                string rcName = cName;
                if (connectors.ContainsKey(cName))
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
            /// <summary>
            /// Adds an output node to the network
            /// </summary>
            /// <param name="oName">Name of output node</param>
            /// <param name="cName">Name of connector</param>
            /// <param name="weight">Weight of connector</param>
            /// <param name="inNode">Name of node to bind connector/node to</param>
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
            /// <summary>
            /// Adds an InputNode to the network
            /// </summary>
            /// <param name="name">Name of node to create</param>
            public void addInput(string name)
            {
                if (inputs.ContainsKey(name))
                {
                    // Do nothing, input key already exists
                }
                else
                {
                    inputs.Add(name, new InputNode(name));
                }
            }
            /// <summary>
            /// Gets the results of a neural network computation
            /// </summary>
            /// <returns>List of values of all OutputNodes</returns>
            public Dictionary<string, float> getResults()
            {
                if (inputs.Count == 0 || outputs.Count == 0)
                {
                    throw new EmptyNodeGroupException();
                }
                Dictionary<string, float> results = new Dictionary<string, float>();
                foreach (KeyValuePair<string, OutputNode> i in outputs)
                {
                    results.Add(i.Key, i.Value.getResult());
                }
                return results;
            }
            /// <summary>
            /// Trigger to start a neural computation
            /// </summary>
            /// <param name="fireNode">Name of InputNode to fire</param>
            public void fireInput(string fireNode)
            {
                if (inputs.Count == 0 || outputs.Count == 0)
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
        /// <summary>
        /// Exception type for missing node parameter
        /// </summary>
        class NodeNotFoundException : Exception
        {

        }
        /// <summary>
        /// Exception for performing operator on empty node group
        /// </summary>
        class EmptyNodeGroupException : Exception
        {

        }
    }
}
