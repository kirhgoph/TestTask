using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp
{
    public class CommandLineArgumentsParser
    {
        public static bool ParseCommandLineArguments(string[] args, ref int _parallelThreadsNumber, ILog log)
        {
            log.Debug("Command line arguments are:");
            foreach (var arg in args)
            {
                log.Debug(arg);
            }
            if (args.Length != 1 || !Int32.TryParse(args[0], out _parallelThreadsNumber))
            {
                log.Debug("Command line arguments are incorrect.");
                System.Console.WriteLine("Please enter the number of parallel calculations.");
                return false;
            }
            log.Debug("Command line arguments are valid.");
            return true;
        }
    }
}
