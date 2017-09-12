using System;
using Microsoft.Owin.Hosting;
using log4net;
using System.Configuration;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SecondApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
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

            
            Console.WriteLine("SecondApp is ready. Press Enter to coninue...");
            Console.ReadLine();
            Console.ReadLine();
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
