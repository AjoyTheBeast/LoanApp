using LoanApp.Services.AuthApi.Models;
using LoanApp.Services.AuthApi.Services;
using LoanApp.Services.AuthApi.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanApp.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        Response response;
        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            response = new Response();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = await _authService.LoginAsync(loginRequest);
            if (string.IsNullOrEmpty(loginResponse.Token))
            {
                response.IsSuccess = false;
                response.Message = "Username or Password is incorrect";
                return BadRequest(response);
            }
            response.Result = loginResponse;
            return Ok(response);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var errorMessage = await _authService.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.IsSuccess = false;
                response.Message = errorMessage;

                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
