using log4net;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using RestSharp;
using Shared;
using System;
using System.Configuration;
using System.Numerics;
using System.Threading.Tasks;

namespace FirstApp
{
    public class MessageBus
    {
        public static IBusControl InitializeMessageBus(IRestServiceRequestSender requestSender, ILog log)
        {
            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(ConfigurationManager.AppSettings["Host"]), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "queue", ep =>
                {
                    ep.Handler<MyMessage>(context =>
                    {
                        return Task.Run(() =>
                        {
                            requestSender.SendRequest(
                                BigInteger.Parse(
                                    ProcessMessage(requestSender, context.Message.Text, log
                                    )));
                        });
                    });
                });
            });

            bus.Start();

            return bus;
        }
        public static string ProcessMessage(IRestServiceRequestSender requestSender, string messageText, ILog log)
        {
            log.Info($"Current number is {messageText}");
            try
            {
                var result = Fibonacci.GetNextNumber(BigInteger.Parse(messageText), log);
                log.Debug($"Next number is {result}");
                return result.ToString();
            }
            catch (Exception exception)
            {
                log.Error("Exception calculating next Fibonacci number", exception);
                throw;
            }
        }
    }
}
