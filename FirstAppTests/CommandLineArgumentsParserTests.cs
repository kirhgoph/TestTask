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

    public class CommandLineArgumentsParserTests
    {
        [TestMethod()]
        public void ParseCommandLineArgumentsDefault()
        {
            var parallelThreadsNumber = 0;
            Assert.IsTrue(CommandLineArgumentsParser.ParseCommandLineArguments(new string[] { "1" }, ref parallelThreadsNumber, _log));
            Assert.AreEqual(1, parallelThreadsNumber);
        }

        [TestMethod()]
        public void ParseCommandLineArgumentsPositive()
        {
            var parallelThreadsNumber = 0;
            Assert.IsTrue(CommandLineArgumentsParser.ParseCommandLineArguments(new string[] { "2" }, ref parallelThreadsNumber, _log));
            Assert.AreEqual(2, parallelThreadsNumber);
        }

        [TestMethod()]
        public void ParseCommandLineArgumentsWrongArgsLength()
        {
            var parallelThreadsNumber = 0;
            Assert.IsFalse(CommandLineArgumentsParser.ParseCommandLineArguments(new string[] { "2", "0" }, ref parallelThreadsNumber, _log));
            Assert.AreEqual(0, parallelThreadsNumber);
        }

        [TestMethod()]
        public void ParseCommandLineArgumentsNotNumber()
        {
            var parallelThreadsNumber = 0;
            Assert.IsFalse(CommandLineArgumentsParser.ParseCommandLineArguments(new string[] { "abc" }, ref parallelThreadsNumber, _log));
            Assert.AreEqual(0, parallelThreadsNumber);
        }
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}