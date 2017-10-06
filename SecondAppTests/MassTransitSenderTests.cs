using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shared;
using MassTransit;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace SecondApp.Tests
{
    [TestClass()]
    public class MassTransitSenderTests
    {
        [TestMethod()]
        public void SendMessageTest()
        {
            var mockBus = new Mock<IBusControl>();
            var text = String.Empty;
            var token = new CancellationTokenSource().Token;
            mockBus.Setup(b => b.Publish(It.IsAny<MassTransitMessage>(), It.IsAny<CancellationToken>()))
                .Callback<MassTransitMessage, CancellationToken>((m,c) => text = m.Text).Returns(Task.FromResult(false));
            var sender = new MassTransitSender(_log, mockBus.Object);
            sender.SendMessage(new MassTransitMessage { Text = "1" });
            mockBus.Verify(b => b.Publish(It.IsAny<MassTransitMessage>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}