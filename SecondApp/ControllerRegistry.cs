using log4net;
using MassTransit;
using Shared;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondApp
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<ILog>().Use(Program._log);
            For<IRequestProcessor>().Use<RequestProcessor>();
            For<IMassTransitSender>().Use<MassTransitSender>().Singleton();
            For<IBusControl>().Use(Program.CreateBus());
            For<IFibonacciHelper>().Use<FibonacciHelper>().Singleton();
        }
    }
}
