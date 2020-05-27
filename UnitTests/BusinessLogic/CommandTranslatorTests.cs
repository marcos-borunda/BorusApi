using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.BusinessLogic;
using WebApi.BusinessLogic.Commands;

namespace UnitTests.BusinessLogic
{
    [TestClass]
    public class CommandTranslatorTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Translate_ServiceIsNull_ThrowArgumentNullException()
        {
            new CommandTranslator(Mock.Of<ILoggerFactory>()).Translate(service: null, "method");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Translate_CommandIsNull_ThrowArgumentNullException()
        {
            new CommandTranslator(Mock.Of<ILoggerFactory>()).Translate("service", command: null);
        }

        [TestMethod]
        public void Translate_ServiceWasNotFound_ReturnNull()
        {
            var result = new CommandTranslator(Mock.Of<ILoggerFactory>()).Translate(service: "thisservicedoesnotexist", command: "datos");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Translate_CommandWasNotFound_ReturnNull()
        {
            var result = new CommandTranslator(Mock.Of<ILoggerFactory>()).Translate(service: "movistar", command: "thismethoddoesnotexist");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Translate_CommandWasFound_ReturnInstance()
        {
            var command = new CommandTranslator(Mock.Of<ILoggerFactory>()).Translate(service: "movistar", command: "datos");
            Assert.IsNotNull(command);
            Assert.IsNull(command.Response);
            Assert.IsInstanceOfType(command, typeof(MovistarDataUsageCommand));
        }
    }
}