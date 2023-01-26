using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServerConnectorLib;

namespace ServerConnectionTests
{
    [TestClass]
    public class ServerConnectionTests
    {
        [TestMethod]
        [DataRow("2.0.0.0", "1.0.0.0", true)]
        [DataRow("1.1.0.0", "1.0.0.0", true)]
        [DataRow("1.0.1.0", "1.0.0.0", true)]
        [DataRow("1.0.0.1", "1.0.0.0", true)]
        [DataRow("1.0.0.0", "2.0.0.0", false)]
        [DataRow("1.0.0.0", "1.2.0.0", false)]
        [DataRow("1.0.0.0", "1.0.2.0", false)]
        [DataRow("1.0.0.0", "1.0.0.2", false)]
        public void IsVersionLatest(string programmVersion, string serverVersion, bool expected)
        {
            var desc = new DesriptionInfo(serverVersion, "QWERTY");
            var mock = new Mock<ServerConnector>("https://sigmageophys.com/Distr", "", "", "", "") { CallBase = true };
            mock.As<IServerConnector>().Setup(p => p.GetDescription()).Returns(desc);
            bool actual = mock.Object.IsVersionLatest(programmVersion);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("1.0.0.0", "1.0.0.0.0", true)]
        [DataRow("1.0.0.0", "1.0.0", true)]
        [DataRow("1.0.0.0", "1.0.0.0", false)]
        public void IsVersionLatestException(string programmVersion, string serverVersion, bool exception)
        {
            var desc = new DesriptionInfo(serverVersion, "QWERTY");
            var mock = new Mock<ServerConnector>("https://sigmageophys.com/Distr", "", "", "", "") { CallBase = true };
            mock.As<IServerConnector>().Setup(p => p.GetDescription()).Returns(desc);
            bool actual;
            try
            {
                mock.Object.IsVersionLatest(programmVersion);
                actual = false;
            }
            catch
            {
                actual = true;
            }            
            Assert.AreEqual(exception, actual);
        }

        [TestMethod]
        [DataRow("hdf5hsdrfdg54sd", "34gheasrg4a3se", false)]
        [DataRow("34gheAsrg4a3se", "34gheasrg4a3se", false)]
        [DataRow("34gheasrg4a3se", "34gheasrg4a3se", true)]
        public void TestIsHashsumEqual(string programmHashsum, string serverHashsum, bool expected)
        {
            var desc = new DesriptionInfo("", serverHashsum);
            var mock = new Mock<ServerConnector>("https://sigmageophys.com/Distr", "", "", "", "") { CallBase = true };
            mock.As<IServerConnector>().Setup(p => p.GetDescription()).Returns(desc);
            bool actual = mock.Object.IsHashsumEqual(programmHashsum);
            Assert.AreEqual(expected, actual);
        }
    }
}
