using log4net;
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
            For<IFibonacciHelper>().Use<FibonacciHelper>().Singleton();
        }
    }
}
