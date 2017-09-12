using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Fibonacci
    {
        public static BigInteger GetNextNumber(BigInteger number, ILog log)
        {
            if (number <= 0) throw new Exception("Number must be positive");
            if (number == 1) return 2;
            BigInteger First = 0, Second = 0, Third = 1;
            while (Second < number)
            {
                First = Second;
                Second = Third;
                try
                {
                    Third = First + Second;
                }
                catch (OutOfMemoryException exception)
                {
                    log.Fatal("There is no free RAM left", exception);
                    throw;
                }
            }
            if (Second > number) throw new Exception("The number given is not a part of the Fibonacci sequence!");
            return Third;
        }
    }

    public class MyMessage { public string Text { get; set; } }
}
