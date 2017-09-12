using Microsoft.VisualStudio.TestTools.UnitTesting;
using FirstApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace FirstApp.Tests
{
    [TestClass()]
    public class MessageBusTests
    {
        [TestMethod()]
        public void ProcessMessageTestDefault()
        {
            var requestSender = new MockRestServiceRequestSender();
            Assert.AreEqual("2", MessageBus.ProcessMessage(requestSender, "1", _log));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ProcessMessageTestException()
        {
            var requestSender = new MockRestServiceRequestSender();
            Assert.AreEqual("2", MessageBus.ProcessMessage(requestSender, "4", _log));
        }
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}