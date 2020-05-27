using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.BusinessLogic;
using WebApi.BusinessLogic.Commands;
using WebApi.BusinessLogic.Services;

namespace UnitTests.Commands
{
    [TestClass]
    public class MovistarDataUsageCommandTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_MovistarIsNull_ThrowsArgumentNullException()
        {
            new MovistarDataUsageCommand(movistar: null, Mock.Of<ILogger<MovistarDataUsageCommand>>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_LoggerIsNull_ThrowsArgumentNullException()
        {
            new MovistarDataUsageCommand(Mock.Of<IMovistar>(), logger: null);
        }

        [TestMethod]
        public void Execute_ReturnsResponse()
        {
            var repository = new MockRepository(MockBehavior.Strict);

            var movistar = repository.Create<IMovistar>();
            movistar.Setup(m => m.GetDataUsage()).Returns("DataUsage");

            var command = new MovistarDataUsageCommand(movistar.Object, Mock.Of<ILogger<MovistarDataUsageCommand>>());
            command.Execute();
            
            Assert.IsNotNull(command.Response);
            Assert.AreEqual(StatusResponse.Ok, command.Response.Status);
            Assert.AreEqual("DataUsage", command.Response.Message);
        }

        [TestMethod]
        public void Execute_MovistarThrowsException_ReturnsErrorResponse()
        {
            var repository = new MockRepository(MockBehavior.Strict);

            var movistar = repository.Create<IMovistar>();
            movistar.Setup(m => m.GetDataUsage()).Throws(new Exception());

            var command = new MovistarDataUsageCommand(movistar.Object, Mock.Of<ILogger<MovistarDataUsageCommand>>());
            command.Execute();
            
            Assert.IsNotNull(command.Response);
            Assert.AreEqual(StatusResponse.Error, command.Response.Status);
            Assert.AreEqual("Error while getting Movistar's data usage.", command.Response.Message);
        }
    }
}