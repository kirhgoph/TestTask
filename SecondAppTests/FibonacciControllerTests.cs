using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Shared;
using log4net;
using System.Web.Http.Results;
using MassTransit;

namespace SecondApp.Tests
{
    [TestClass()]
    public class FibonacciControllerTests
    {
        [ClassInitialize]
        public static void InitializeMessageBus(TestContext testContext)
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                _log.Debug("Creating Host");
                var host = sbc.Host(new Uri(_hostUri), h =>
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
                        });
                    });
                });
            });

            _log.Info("MessageBus ready");
            _busControl.Start();
        }

        [ClassCleanup]
        public static void CleanUp()
        { 
            _busControl.Stop();
        }

        [TestMethod()]
        public void GetNextFibonacciNumberTest()
        {
            var requestProcessor = new RequestProcessor(new FibonacciHelper(_log), _log);
            var controller = new FibonacciController(requestProcessor, new MassTransitSender(_log, _busControl), _log);
            var result = controller.GetNextFibonacciNumber("1") as OkNegotiatedContentResult<String>;
            Assert.AreEqual("2", result.Content);
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static IBusControl _busControl;
        static string _hostUri = @"rabbitmq://localhost/";
    }
}