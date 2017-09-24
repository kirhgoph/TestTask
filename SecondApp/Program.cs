using System;
using Microsoft.Owin.Hosting;
using log4net;
using System.Configuration;
using System.Web.Http;
using Owin;
using StructureMap;
using Shared;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using MassTransit.Util;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SecondApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            _log.Info("Building IoC container");
            try
            {
                _container = new Container(_ =>
                {
                    _.For<ILog>().Use(_log);
                    _.For<IFibonacciHelper>().Use<FibonacciHelper>();
                    _.For<IBusControl>().Use(CreateBus());
                    _.Scan(x =>
                    {
                        x.TheCallingAssembly();
                        x.WithDefaultConventions();
                    });
                });
            }
            catch (Exception exception)
            {
                _log.Fatal("Failed to build IoC container", exception);
            }
            _log.Info("Starting ApiController");
            try
            {
                WebApp.Start<ApiControllerStartup>(url: ConfigurationManager.AppSettings["WebApiAddress"]);
                _log.Info("ApiController started");
            }
            catch (Exception exception)
            {
                _log.Fatal("Failed to start ApiController", exception);
            }
            _container.AssertConfigurationIsValid();
            Console.WriteLine("SecondApp is ready. Press Enter to coninue...");
            Console.ReadLine();
            Console.ReadLine();
        }

        public static IBusControl CreateBus()
        {
            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri(_hostUri), h =>
            {
                h.Username("guest");
                h.Password("guest");
            }));
            TaskUtil.Await(() => bus.StartAsync());
            return bus;
        }

        public static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Container _container;
        static string _hostUri = ConfigurationManager.AppSettings["Host"];
    }
}
