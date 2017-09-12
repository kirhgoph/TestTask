using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp
{
    public class RestServiceRequestSender : IRestServiceRequestSender
    {
        public RestServiceRequestSender(RestClient restClient)
        {
            _restClient = restClient;
        }

        public RestRequest SendRequest(BigInteger requestBody)
        {
            var request = new RestRequest(requestBody.ToString(), Method.GET);
            _restClient.ExecuteAsync(request, response => { });
            return request;
        }
        private RestClient _restClient;
    }

    public class MockRestServiceRequestSender : IRestServiceRequestSender
    {
        public RestRequest SendRequest(BigInteger requestBody)
        {
            var request = new RestRequest(requestBody.ToString(), Method.GET);
            request.Resource = requestBody.ToString();
            return request;
        }
    }

    public interface IRestServiceRequestSender
    {
        RestRequest SendRequest(BigInteger requestBody);
    }
}
