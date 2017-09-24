using log4net;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using Shared;
using System;
using System.Configuration;
using System.Numerics;
using System.Threading.Tasks;

namespace FirstApp
{
    public class MessageBus : IMessageBus
    {
        public MessageBus(IFibonacciHelper fibonacci, IRestServiceRequestSender requestSender, ILog log)
        {
            _fibonacci = fibonacci;
            _requestSender = requestSender;
            _log = log;
        }

        public IBusControl InitializeMessageBus()
        {
            Log4NetLogger.Use();
            _log.Info("Creating MessageBus");
            var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                _log.Debug("Creating Host");
                var host = sbc.Host(new Uri(ConfigurationManager.AppSettings["Host"]), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                _log.Debug("Creating ReceiveEndpoint");
                sbc.ReceiveEndpoint(host, "queue", ep =>
                {
                    ep.Handler<MassTransitMessage>(context =>
                    {
                        return Task.Run(() =>
                        {
                            _requestSender.SendRequest(
                                BigInteger.Parse(
                                    ProcessMessage(context.Message.Text
                                    )));
                        });
                    });
                });
            });
            _log.Info("MessageBus ready");

            busControl.Start();
            _log.Info("MessageBus started");

            return busControl;
        }
        public string ProcessMessage(string messageText)
        {
            _log.Info($"Current number is {messageText}");
            try
            {
                var result = _fibonacci.GetNextNumber(BigInteger.Parse(messageText));
                _log.Debug($"Next number is {result}");
                return result.ToString();
            }
            catch (Exception exception)
            {
                _log.Error("Exception calculating next Fibonacci number", exception);
                throw;
            }
        }
        private IFibonacciHelper _fibonacci;
        private IRestServiceRequestSender _requestSender;
        private ILog _log;
    }

    public interface IMessageBus
    {
        IBusControl InitializeMessageBus();
        string ProcessMessage(string messageText);
    }
}
