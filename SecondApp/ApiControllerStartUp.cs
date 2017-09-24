using Owin;
using System;
using System.Web.Http;
using WebApi.StructureMap;

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
                config.UseStructureMap<ControllerRegistry>();
                appBuilder.UseWebApi(config);
                log4net.Config.XmlConfigurator.Configure();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
