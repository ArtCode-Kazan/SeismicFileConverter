using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace ServerConnectionTests
{
    [TestClass]
    public class ServerConnectionTests
    {
        [TestMethod]
        public void TestDescription()
        {
            // Arrange
            var mock = new Mock<ServerConnector>();
            mock.Setup(a => a.GetComputerList()).Returns(new List<Computer>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }
    }
}
