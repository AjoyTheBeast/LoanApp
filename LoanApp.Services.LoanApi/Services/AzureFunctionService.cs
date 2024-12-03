using LoanApp.Services.LoanApi.Models.DTO;
using LoanApp.Services.LoanApi.Services.IService;
using Newtonsoft.Json;
using System.Text;

namespace LoanApp.Services.LoanApi.Services
{
    public class AzureFunctionService : IAzureFunctionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AzureFunctionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> ValidateLoanDetails(LoanRequestDTO request)
        {
            HttpClient client = _httpClientFactory.CreateClient("AzureFunction");
            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");
            message.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            message.Method = HttpMethod.Post;
            var response = await client.SendAsync(message);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(content);
        }
    }
}
