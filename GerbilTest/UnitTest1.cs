using System;
using System.Net;
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
        [TestMethod]
        public void TestServiceInit()
        {
            Gerbil.Gerbil_PortServices.PortLookup.initServices();
            int[] testPorts = { 80 };
            Assert.AreEqual(Gerbil.Gerbil_PortServices.PortLookup.getServices(testPorts)[0], "HTTP");
            Gerbil.Gerbil_PortServices.PortLookup.createService("TestService", 5000);
            Gerbil.Gerbil_PortServices.PortLookup.removeService("TestService", 5000);
            Gerbil.Gerbil_PortServices.PortLookup.launch("add", "TestService", "5000");
            Gerbil.Gerbil_PortServices.PortLookup.launch("remove", "TestService", "5000");
            Assert.IsNotNull(Gerbil.Gerbil_PortServices.PortLookup.getPorts());
        }
        [TestMethod]
        public void TestDatabaseService()
        {
            Gerbil.Data.Database<int> db = new Gerbil.Data.Database<int>("Test DB");
            db.Create(1);
            Assert.AreEqual(db.itemcount, 1);
            Assert.AreEqual(db.Read(db.getAllIDs()[0]), 1);
            db.Update(db.getAllIDs()[0], 2);
            db.Delete(db.getAllIDs()[0]);
        }
        [TestMethod]
        public void TestScanners()
        {
            IPAddress me = IPAddress.Loopback;
            Gerbil.AttackMethods.begin(me.ToString(), 85, 1000);
            Gerbil.AttackMethods.begin(me.ToString(), 85, 87, 1000);
        }
    }
}
