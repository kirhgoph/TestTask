using System;
using System.Configuration;
using RestSharp;
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

                if (!CommandLineArgumentsParser.ParseCommandLineArguments(args, ref _parallelThreadsNumber, _log)) return;

                _log.Info("Build IoC container");
                var container = new Container(_ =>
                {
                    _.Scan(x =>
                    {
                        x.TheCallingAssembly();
                        x.WithDefaultConventions();
                    });
                });

                _log.Info("Creating REST client");
                _restClient = new RestClient(ConfigurationManager.AppSettings["RestServiceAddress"]);
                _requestSender = new RestServiceRequestSender(_restClient);
                /*_requestSender = _container.GetInstance<IRestServiceRequestSender>()
                    .ShouldBeOfType<RestServiceRequestSender>
                    .RestClient.ShouldBe(_restClient);*/

                _log.Info("Initializing Message Bus");
                _bus = MessageBus.InitializeMessageBus(_requestSender, _log);

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
                    _bus.Stop();
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
        static RestClient _restClient;
        static int _parallelThreadsNumber = 1;
        static IBusControl _bus;
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Container _container;
    }
}
