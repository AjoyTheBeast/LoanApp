using LoanApp.Services.AuthApi.Models;

namespace LoanApp.Services.AuthApi.Services.IService
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest loginRequest);
        Task<string> RegisterAsync(RegistrationRequest registrationRequest);
    }
}
