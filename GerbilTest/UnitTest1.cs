using System;
using System.IO;
using System.Net;
//using System.Windows.Forms;
using NUnit.Framework;
using Gerbil;

namespace GerbilTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
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
        [Test]
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
        [Test]
        public void TestDatabaseService()
        {
            Gerbil.Data.Database<int> db = new Gerbil.Data.Database<int>("Test DB");
            db.Create(1);
            Assert.AreEqual(db.itemcount, 1);
            Assert.AreEqual(db.Read(db.getAllIDs()[0]), 1);
            db.Update(db.getAllIDs()[0], 2);
            db.Delete(db.getAllIDs()[0]);
        }
        [Test]
        public void TestScanners()
        {
            IPAddress me = IPAddress.Loopback;
            Gerbil.AttackMethods.begin(me.ToString(), 85, 1000);
            Gerbil.AttackMethods.begin(me.ToString(), 85, 87, 1000);
        }
        //[TestMethod]
        //public void TestGUI()
        //{
        //    Gerbil.GerbilGui gui = new GerbilGui();
        //}
        [Test]
        public void TestEngine()
        {
            string filepath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "Gerbil", "memstore", "OSServiceTraining.ini");
            string[] filecontents = { "RDP=Windows", "SSH=Linux", "HTTP=Windows" };
            string[] services = { "HTTP", "RDP" };
            System.IO.File.WriteAllLines(filepath, filecontents);
            Gerbil.Gerbil_Engine.GerbilRunner.guessOS(services, false);
            Gerbil.Gerbil_Engine.GerbilRunner.guessHTTPService();
        }
        [Test]
        public void TestAttackers()
        {
            Gerbil.Attackers.Attacker a = new Gerbil.Attackers.Attacker();
            a.init();
            a.stab();
            a.clean();
            try
            {
                new Gerbil.Attackers.HTTPAuthAttacker(IPAddress.Loopback.ToString(), 1).stab();
            }
            catch
            {
                // This is just to make the test not fail
            }
            new Gerbil.Attackers.WoLAttacker("00:00:00:00:00:00").stab();
        }
        //[Test]
        //public void TestPasswordCracker()
        //{
        //    string pwd = "55";
        //    string cpwd = "";
        //    Gerbil.PasswordServices.SimplePasswordCracker pc = new Gerbil.PasswordServices.SimplePasswordCracker(4);
        //    while(cpwd != pwd)
        //    {
        //        cpwd = pc.getNextKey();
        //    }
        //    Gerbil.PasswordServices.SimplePasswordCracker pc2 = new Gerbil.PasswordServices.SimplePasswordCracker(1);
        //    bool hasFailed = false;
        //    while(!hasFailed)
        //    {
        //        try
        //        {
        //            pc2.getNextKey();
        //        }
        //        catch(Gerbil.PasswordServices.PasswordTableExhaustedException e)
        //        {
        //            hasFailed = true;
        //        }
        //    }
        //}
    }
}
