using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Shared.Tests
{
    [TestClass()]
    public class SharedTests
    {
        [TestMethod()]
        public void GetNextNumberTestPositive()
        {
            var fibonacci = new FibonacciHelper(_log);
            var result = fibonacci.GetNextNumber(1);
            Assert.AreEqual(2, result);
            result = fibonacci.GetNextNumber(2);
            Assert.AreEqual(3, result);
            result = fibonacci.GetNextNumber(3);
            Assert.AreEqual(5, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetNextNumberTestNegative()
        {
            var fibonacci = new FibonacciHelper(_log);
            var result = fibonacci.GetNextNumber(1);
            Assert.AreEqual(2, result);
            result = fibonacci.GetNextNumber(2);
            Assert.AreEqual(3, result);
            result = fibonacci.GetNextNumber(3);
            Assert.AreEqual(5, result);
            result = fibonacci.GetNextNumber(4);
        }
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}