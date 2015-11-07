using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gerbil;

namespace GerbilTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNeuralNet()
        {
            Gerbil.NeuralNetwork.Network net = new Gerbil.NeuralNetwork.Network();
            net.addInput("test");
            net.addNode("itest", "ctest", 1, "test");
            net.addOutput("testout", "ctest2", 1, "itest");
            net.fireInput("test");
            float testVal;
            Assert.IsTrue(net.getResults().TryGetValue("testout", out testVal));
            Assert.IsNotNull(testVal);
        }
    }
}
