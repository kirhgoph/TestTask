using MassTransit;
using System;
using System.Configuration;
using MassTransit.Util;
using Shared;
using log4net;
using MassTransit.Log4NetIntegration.Logging;

namespace SecondApp
{
    internal static class MassTransitSender
    {
        static IBusControl _busControl;
        static MassTransitSender()
        {
            try
            {
                _log.Info("Creating bus");
                _busControl = CreateBus();
            }
            catch (Exception exception)
            {
                _log.Fatal("Could not create Message Bus!", exception);
            }
            try
            {
                TaskUtil.Await(() => _busControl.StartAsync());
            }
            catch (Exception exception)
            {
                _log.Fatal("Could not start Message Bus!", exception);
            }
        }
        private static IBusControl CreateBus()
        {
            Log4NetLogger.Use();
            return Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri(ConfigurationManager.AppSettings["Host"]), h =>
            {
                h.Username("guest");
                h.Password("guest");
            }));
        }
        public static void SendMessage(MyMessage message)
        {
            _log.Info("Sending message");
            _busControl.Publish(message);
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
