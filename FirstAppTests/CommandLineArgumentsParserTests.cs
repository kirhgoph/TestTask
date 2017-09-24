using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.IsTrue(_parser.ParseCommandLineArguments(new string[] { "1" }, ref parallelThreadsNumber));
            Assert.AreEqual(1, parallelThreadsNumber);
        }

        [TestMethod()]
        public void ParseCommandLineArgumentsPositive()
        {
            var parallelThreadsNumber = 0;
            Assert.IsTrue(_parser.ParseCommandLineArguments(new string[] { "2" }, ref parallelThreadsNumber));
            Assert.AreEqual(2, parallelThreadsNumber);
        }

        [TestMethod()]
        public void ParseCommandLineArgumentsWrongArgsLength()
        {
            var parallelThreadsNumber = 0;
            Assert.IsFalse(_parser.ParseCommandLineArguments(new string[] { "2", "0" }, ref parallelThreadsNumber));
            Assert.AreEqual(0, parallelThreadsNumber);
        }

        [TestMethod()]
        public void ParseCommandLineArgumentsNotNumber()
        {
            var parallelThreadsNumber = 0;
            Assert.IsFalse(_parser.ParseCommandLineArguments(new string[] { "abc" }, ref parallelThreadsNumber));
            Assert.AreEqual(0, parallelThreadsNumber);
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CommandLineArgumentsParser _parser = new CommandLineArgumentsParser(_log);
    }
}