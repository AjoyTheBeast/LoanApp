using LoanApp.Services.AuthApi.Data;
using LoanApp.Services.AuthApi.Models;
using LoanApp.Services.AuthApi.Services.IService;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LoanApp.Services.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator tokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = tokenGenerator;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginRequest.UserName);
            if(user != null)
            {
                bool checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if(checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _jwtTokenGenerator.GenerateToken(user, roles);
                    LoginResponse loginResponse = new()
                    {
                        Token = jwtToken,
                    };
                    return loginResponse;
                }
            }
            return new LoginResponse { Token = "" };
        }

        public async Task<string> RegisterAsync(RegistrationRequest registrationRequest)
        {
            string role = registrationRequest.Role;
            ApplicationUser user = new()
            {
                UserName = registrationRequest.Email,
                Email = registrationRequest.Email,
                NormalizedEmail = registrationRequest.Email.ToUpper(),
                Name = registrationRequest.Name,
                PhoneNumber = registrationRequest.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequest.Password);
                if(result.Succeeded)
                {
                    var addedUser = await _userManager.FindByEmailAsync(user.Email);
                    if(addedUser != null)
                    {
                        if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }
                        await _userManager.AddToRoleAsync(user, role);
                    }
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return "Error Encountered";
            }
        }
    }
}
