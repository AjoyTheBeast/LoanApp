using LoanApp.Web.Models;
using LoanApp.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoanApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoanService _loanService;
        public HomeController(ILogger<HomeController> logger, ILoanService loanService)
        {
            _logger = logger;
            _loanService = loanService;
        }
        [Authorize(Roles ="Customer")]
        public IActionResult Index()
        {
            var model = new LoanRequest
            {
                LoanAmount = 10000 // Set a default or previously submitted value
            };
            return View(model);
        }
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> LoanRequest([FromBody] LoanRequest loanRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _loanService.CreateLoanRequest(loanRequest);
                    if (response.IsSucess)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(new { message = response.Message });
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
