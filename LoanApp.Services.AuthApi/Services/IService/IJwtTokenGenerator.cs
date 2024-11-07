using LoanApp.Services.AuthApi.Models;

namespace LoanApp.Services.AuthApi.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
