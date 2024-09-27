using LoanApp.Web.Models;
using LoanApp.Web.Service.IService;
using Newtonsoft.Json;
using System.Text;
using static LoanApp.Web.Utility.SD;

namespace LoanApp.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<Response> SendAsync(Request request)
        {
            var client = httpClientFactory.CreateClient("loan");
            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");

            message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");

            message.RequestUri = new Uri(request.Url);

            HttpResponseMessage? apiResponse = null;

            switch (request.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var response =  JsonConvert.DeserializeObject<Response>(apiContent);
            return response;
        }
    }
}
