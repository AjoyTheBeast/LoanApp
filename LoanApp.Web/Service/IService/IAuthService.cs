using LoanApp.Web.Models;

namespace LoanApp.Web.Service.IService
{
    public interface IAuthService
    {
        public Task<Response> LoginAsync(LoginRequest loginRequest);
        Task<Response> RegisterAsync(RegistrationRequest registrationRequest);
    }
}