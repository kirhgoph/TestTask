using log4net;
using System;

namespace FirstApp
{
    public class CommandLineArgumentsParser : ICommandLineArgumentsParser
    {
        public CommandLineArgumentsParser(ILog log)
        {
            _log = log;
        }

        public bool ParseCommandLineArguments(string[] args, ref int _parallelThreadsNumber)
        {
            _log.Debug("Command line arguments are:");
            foreach (var arg in args)
            {
                _log.Debug(arg);
            }
            if (args.Length != 1 || !Int32.TryParse(args[0], out _parallelThreadsNumber))
            {
                _log.Debug("Command line arguments are incorrect.");
                Console.WriteLine("Please enter the number of parallel calculations.");
                return false;
            }
            _log.Debug("Command line arguments are valid.");
            return true;
        }
        private ILog _log;
    }

    public interface ICommandLineArgumentsParser
    {
        bool ParseCommandLineArguments(string[] args, ref int _parallelThreadsNumber);
    }
}