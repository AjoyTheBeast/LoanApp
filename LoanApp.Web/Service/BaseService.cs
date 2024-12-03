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
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            this.httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<Response> SendAsync(Request request, bool withBearer)
        {
            var client = httpClientFactory.CreateClient("loan");
            HttpRequestMessage message = new();

            if(request.ContentType == ContentType.MultipartFormData)
                message.Headers.Add("Accept", "*/*");
            else
                message.Headers.Add("Accept", "application/json");

            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            if(request.ContentType == ContentType.MultipartFormData)
            {
                var content = new MultipartFormDataContent();
                foreach(var prop in request.Data.GetType().GetProperties())
                {
                    var value = prop.GetValue(request.Data);
                    if(value is FormFile)
                    {
                        var file = (FormFile)value;
                        if(file != null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                    }
                }
                message.Content = content;
            }
            else
            {
                if(request.Data != null)
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
            }

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
