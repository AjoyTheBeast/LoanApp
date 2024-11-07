using LoanApp.Web.Models;
using LoanApp.Web.Service.IService;
using LoanApp.Web.Utility;

namespace LoanApp.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<Response> LoginAsync(LoginRequest loginRequest)
        {
            return await _baseService.SendAsync(new Request()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "api/auth/login",
                Data = loginRequest
            });
        }

        public async Task<Response> RegisterAsync(RegistrationRequest registrationRequest)
        {
            return await _baseService.SendAsync(new Request()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthApiBaseUrl + "api/auth/register",
                Data = registrationRequest
            });
        }
    }
}
