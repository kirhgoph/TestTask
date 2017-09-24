using System;
using System.Configuration;
using Shared;
using MassTransit;
using log4net;
using StructureMap;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace FirstApp
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                _log.Info("Application start");

                _log.Info("Build IoC container");
                _container = new Container(_ =>
                {
                    _.For<ILog>().Use(_log);
                    _.For<IFibonacciHelper>().Use<FibonacciHelper>().Singleton();
                    _.For<IRestServiceRequestSender>().Use<RestServiceRequestSender>().Ctor<string>("serviceAddress").
                        Is(ConfigurationManager.AppSettings["RestServiceAddress"]);
                    _.Scan(x =>
                    {
                        x.TheCallingAssembly();
                        x.WithDefaultConventions();
                    });
                });

                var commandLineArgumentsParser = _container.GetInstance<ICommandLineArgumentsParser>();
                if (!commandLineArgumentsParser.ParseCommandLineArguments(args, ref _parallelThreadsNumber))
                    return;

                _log.Info("Creating REST client");
                _requestSender = _container.GetInstance<IRestServiceRequestSender>();

                _log.Info("Initializing Message Bus");
                var messageBus = _container.GetInstance<IMessageBus>();
                _busControl = messageBus.InitializeMessageBus();

                _container.Configure(cfg =>
                {
                    cfg.For<IBusControl>()
                        .Use(_busControl);
                    cfg.Forward<IBusControl, IBus>();
                });

                Console.WriteLine("Press Enter when the SecondApp is ready");
                Console.ReadLine();

                SendInitialRestRequests(_parallelThreadsNumber, _requestSender, _log);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey(); 
            }
            catch (Exception exception)
            {
                _log.Fatal("Uncaught exception occured", exception);
            }
            finally
            {
                try
                {
                    _log.Info("Stopping bus...");
                    _busControl.Stop();
                }
                catch (Exception exception)
                {
                    _log.Fatal("Uncaught exception occured", exception);
                }
            }
        }

        private static void SendInitialRestRequests(int parallelThreadsNumber, IRestServiceRequestSender requestSender, ILog _log)
        {
            for (int i = 0; i < parallelThreadsNumber; i++)
                requestSender.SendRequest(1);
        }

        static IRestServiceRequestSender _requestSender;
        static int _parallelThreadsNumber = 1;
        static IBusControl _busControl;
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Container _container;
    }
}