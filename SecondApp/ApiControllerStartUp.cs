using log4net;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SecondApp
{
    public class ApiControllerStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            try
            {
                HttpConfiguration config = new HttpConfiguration();
                config.MapHttpAttributeRoutes();
                appBuilder.UseWebApi(config);
                log4net.Config.XmlConfigurator.Configure();
                _log.Info("ApiController configuration succeded");
            }
            catch (Exception exception)
            {
                _log.Error("ApiController configuration failed", exception);
                throw exception;
            }
        }
        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
