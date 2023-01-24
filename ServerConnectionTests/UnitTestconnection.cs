using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ServerConnectorLib;

namespace ServerConnectionTests
{
    [TestClass]
    public class ServerConnectionTests
    {
        [TestMethod]
        [DataRow("1.0.0.0", "1.0.0.0", "1.0.0.0")]
        [DataRow("1.2.0.0", "1.0.0.0", "1.2.0.0")]
        [DataRow("5.0.0.0", "1.0.0.0", "5.0.0.0")]
        [DataRow("0.0.0.0", "0.0.0.0", "0.0.0.0")]
        [DataRow("9.0.0.0", "1.0.0.0", "9.0.0.0")]
        [DataRow("1.0.0.4", "1.0.0.0", "1.0.0.4")]
        [DataRow("1.745.756.0", "1.0.0.0", "1.745.756.0")]
        public void TestEqualVersion(string first, string second, string expections)
        { 
            ServerConnector server = new ServerConnector("https://sigma-geophys.com/Disrt/", "");
            string result = server.LatestVersion(first, second);
            Assert.AreEqual(result, expections);
        }

        [TestMethod]         
        [DataRow("1.0.0.0.4", "1.0.0.0", "1.0.0.4")]
        [DataRow("1.0.4", "1.0.0.0", "1.0.0.4")]
        [DataRow("1.0.0.4", "1.0.0.0", "1.0.0.4")]
        public void TestEqualVersionException(string first, string second, string expections)
        {
            ServerConnector server = new ServerConnector("https://sigma-geophys.com/Disrt/", "");
            string result = server.LatestVersion(first, second);
            Assert.AreEqual(result, expections);
        }

        [TestMethod]
        [DataRow("1.0.0.0", "1.0.0.0", true)]
        [DataRow("hdf5hsdrfdg54sd", "34gheasrg4a3se", false)]
        [DataRow("34gheAsrg4a3se", "34gheasrg4a3se", false)]
        [DataRow("34gheasrg4a3se", "34gheasrg4a3se", true)]
        public void TestEqual(string first, string second, bool expections)
        {
            ServerConnector server = new ServerConnector("https://sigma-geophys.com/Disrt/", "");
            bool result = server.IsEqual(first, second);
            Assert.AreEqual(result, expections);
        }
    }
}
