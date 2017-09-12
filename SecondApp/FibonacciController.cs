using System;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SecondApp
{
    [RoutePrefix("fibonacci")]
    public class FibonacciController : ApiController
    {
        
        [Route("{number}")]
        [HttpGet]
        public IHttpActionResult GetNextFibonacciNumber(string number)
        {
            try
            {
                RequestProcessor.ProcessRequest(number, _log);
                MassTransitSender.SendMessage(
                    new MyMessage
                    {
                        Text = RequestProcessor.ProcessRequest(number, _log)
                    });
                return Ok();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
