using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.BusinessLogic;
using WebApi.BusinessLogic.Commands;

namespace UnitTests.BusinessLogic
{
    [TestClass]
    public class InvokerTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_CommandIsNull_ThrowNewArgumentNullException()
        {
            new Invoker(command: null);
        }

        [TestMethod]
        public void Invoke_CommandResponseIsNull_ReturnsErrorResponse()
        {
            var repository = new MockRepository(MockBehavior.Strict);
            
            var command = repository.Create<ICommand>();
            command.Setup(c => c.Execute(Enumerable.Empty<string>()));
            command.Setup(c => c.Response).Returns<Response>(default);

            var actualResponse = new Invoker(command.Object).Invoke(Enumerable.Empty<string>());

            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(StatusResponse.Error, actualResponse.Status);
            Assert.AreEqual("Error while invoking command.", actualResponse.Message);
        }

        [TestMethod]
        public void Invoke_CommandResponseIsNotNull_ReturnsSameResponse()
        {
            var repository = new MockRepository(MockBehavior.Strict);
            
            var expectedResponse = new Response(StatusResponse.Ok, message: "Correct response.");

            var command = repository.Create<ICommand>();
            command.Setup(c => c.Execute(Enumerable.Empty<string>()));
            command.Setup(c => c.Response).Returns(expectedResponse);

            var actualResponse = new Invoker(command.Object).Invoke(Enumerable.Empty<string>());

            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}