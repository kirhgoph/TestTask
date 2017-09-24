using log4net;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Shared
{
    public class FibonacciHelper : IFibonacciHelper
    {
        public FibonacciHelper(ILog log)
        {
            _log = log;
        }

        public BigInteger GetNextNumber(BigInteger number)
        {
            if (number <= 0) throw new Exception("Number must be positive");
            _log.Debug("Number is positive. Proceeding");
            lock ("newLock")
            {
                var index = cache.FindLastIndex(element => element == number);
                _log.Debug($"Index of given number is:{index}");
                if (index != -1 && index < cache.Count - 1) return cache[index + 1];
                _log.Debug($"Number was not found in cache");
                if (index == -1 && (cache[cache.Count - 1] + cache[cache.Count - 2] == number))
                {
                    _log.Debug($"Number is the next number in sequence. Adding to cache");
                    cache.Add(number);
                    index = cache.Count - 1;
                }
                if (index ==cache.Count -1)
                {
                    _log.Debug($"Calculating new number in sequence");
                    var newNumber = cache[cache.Count - 1] + cache[cache.Count - 2];
                    cache.Add(newNumber);
                    return cache[cache.Count-1];
                }
                else throw new Exception("The number given is not a part of the Fibonacci sequence!");
            }
        }

        static private List<BigInteger> cache = new List<BigInteger> { 1, 2 };
        private ILog _log;
    }

    public interface IFibonacciHelper
    {
        BigInteger GetNextNumber(BigInteger number);
    }

    [Serializable]
    public class MassTransitMessage { public string Text { get; set; } }
}
