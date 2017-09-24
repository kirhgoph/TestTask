using System;
using Shared;
using log4net;
using System.Numerics;

namespace SecondApp
{
    public class RequestProcessor : IRequestProcessor
    {
        public RequestProcessor(IFibonacciHelper fibonacciHelper, ILog log)
        {
            _log = log;
            _fibonacciHelper = fibonacciHelper;
        }
        public string ProcessRequest(string number)
        {
            try
            {
                _log.Info("Processing the request");
                _log.Debug($"Number is: {number}");
                var result = _fibonacciHelper.GetNextNumber(BigInteger.Parse(number));
                return result.ToString();
            }
            catch (Exception exception)
            {
                _log.Error("Failed to process request", exception);
                throw;
            }
        }

        private ILog _log;
        private IFibonacciHelper _fibonacciHelper;
    }

    public interface IRequestProcessor
    {
        string ProcessRequest(string number);
    }
}
