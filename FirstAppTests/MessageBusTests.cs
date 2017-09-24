using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net;
using Shared;
using System.Numerics;
using Moq;

namespace FirstApp.Tests
{
    [TestClass()]
    public class MessageBusTests
    {
        [TestInitialize]
        public void TestInitialization()
        {
            _fibonacciHelper.Setup(x => x.GetNextNumber(new BigInteger(1))).Returns(new BigInteger(2));
            _fibonacciHelper.Setup(x => x.GetNextNumber(new BigInteger(2))).Returns(new BigInteger(3));
        }

        [TestMethod()]
        public void ProcessMessageTestDefault()
        {
            var _messageBus = new MessageBus(_fibonacciHelper.Object, new RestServiceRequestSender(_log, "http://uri"), _log);
            Assert.AreEqual("2", _messageBus.ProcessMessage("1"));
        }

        Mock<IFibonacciHelper> _fibonacciHelper = new Mock<IFibonacciHelper>();
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}