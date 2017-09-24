using log4net;
using RestSharp;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace FirstApp
{
    public class RestServiceRequestSender : RestClient, IRestServiceRequestSender
    {
        public RestServiceRequestSender(ILog log, string serviceAddress)
        {
            BaseUrl = new Uri(serviceAddress);
            _log = log;
        }

        public async Task<RestRequest> SendRequest(BigInteger requestBody)
        {
            _log.Debug($"Request body:{requestBody}");
            var request = new RestRequest(requestBody.ToString(), Method.GET);
            var cancellationTokenSource = new CancellationTokenSource();
            _log.Info($"Sending request");
            var handle = await ExecuteTaskAsync(request, cancellationTokenSource.Token);
            return request;
        }

        private ILog _log;
    }

    public interface IRestServiceRequestSender : IRestClient
    {
        Task<RestRequest> SendRequest(BigInteger requestBody);
    }
}
