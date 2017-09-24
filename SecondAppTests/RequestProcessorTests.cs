using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Shared;

namespace SecondApp.Tests
{
    [TestClass()]
    public class RequestProcessorTests
    {
        [TestMethod()]
        public void ProcessRequestTestDefault()
        {
            Assert.AreEqual("2", requestProcessor.ProcessRequest("1"));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ProcessRequestTestException()
        {
            Assert.AreEqual("2", requestProcessor.ProcessRequest("4"));
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        RequestProcessor requestProcessor = new RequestProcessor(new FibonacciHelper(_log), _log);
    }
}