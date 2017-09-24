using MassTransit;
using System;
using System.Configuration;
using MassTransit.Util;
using Shared;
using log4net;

namespace SecondApp
{
    public class MassTransitSender : IMassTransitSender
    {
        public MassTransitSender(ILog log, IBusControl busControl)
        {
            _log = log;
            _busControl = busControl;
        }
        public async void SendMessage(MassTransitMessage message)
        {
            _log.Info("Sending message");
            await _busControl.Publish(message);
        }

        IBusControl _busControl;
        ILog _log;
        string _hostUri = ConfigurationManager.AppSettings["Host"];
    }

    public interface IMassTransitSender
    {
        void SendMessage(MassTransitMessage message);
    }
}
