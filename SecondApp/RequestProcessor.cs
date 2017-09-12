using MassTransit;
using System;
using System.Configuration;
using MassTransit.Util;
using Shared;
using log4net;
using MassTransit.Log4NetIntegration.Logging;
using System.Numerics;

namespace SecondApp
{
    public static class RequestProcessor
    {
        public static string ProcessRequest(string number, ILog log)
        {
            try
            {
                var result = Fibonacci.GetNextNumber(BigInteger.Parse(number), log);
                return result.ToString();
            }
            catch (Exception exception)
            {
                log.Error("Failed to process request", exception);
                throw;
            }
        }
    }
}
