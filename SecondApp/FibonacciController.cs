using System;
using Shared;
using System.Web.Http;
using log4net;

namespace SecondApp
{
    [RoutePrefix("fibonacci")]
    public class FibonacciController : ApiController
    {
        public FibonacciController(IRequestProcessor requestProcessor, IMassTransitSender massTransitSender, ILog log)
        {
            _requestProcessor = requestProcessor;
            _log = log;
            _massTransitSender = massTransitSender;
        }

        [Route("{number}")]
        [HttpGet]
        public IHttpActionResult GetNextFibonacciNumber(string number)
        {
            try
            {
                _log.Info("Procesing the request");
                var text = _requestProcessor.ProcessRequest(number);
                _log.Debug("Sending MessageBus' message");
                _massTransitSender.SendMessage(
                    new MassTransitMessage
                    {
                        Text = text
                    });
                return Ok(text);
            }
            catch (Exception exception)
            {
                _log.Error("Failed to process the request", exception);
                return InternalServerError(exception);
            }
        }

        private ILog _log;
        private IRequestProcessor _requestProcessor;
        private IMassTransitSender _massTransitSender;
    }
}
