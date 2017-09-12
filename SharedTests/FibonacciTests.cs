using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Shared.Tests
{
    [TestClass()]
    public class FibonacciTests
    {
        [TestMethod()]
        public void GetNextNumberTestPositive()
        {
            var result = Fibonacci.GetNextNumber(1, _log);
            Assert.AreEqual(2, result);
            result = Fibonacci.GetNextNumber(2, _log);
            Assert.AreEqual(3, result);
            result = Fibonacci.GetNextNumber(3, _log);
            Assert.AreEqual(5, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetNextNumberTestNegative()
        {
            var result = Fibonacci.GetNextNumber(1, _log);
            Assert.AreEqual(2, result);
            result = Fibonacci.GetNextNumber(2, _log);
            Assert.AreEqual(3, result);
            result = Fibonacci.GetNextNumber(3, _log);
            Assert.AreEqual(5, result);
            result = Fibonacci.GetNextNumber(4, _log);
        }
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}