using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecondApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondApp.Tests
{
    [TestClass()]
    public class RequestProcessorTests
    {
        [TestMethod()]
        public void ProcessRequestTestDefault()
        {
            Assert.AreEqual("2", RequestProcessor.ProcessRequest("1", _log));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ProcessRequestTestException()
        {
            Assert.AreEqual("2", RequestProcessor.ProcessRequest("4", _log));
        }
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}