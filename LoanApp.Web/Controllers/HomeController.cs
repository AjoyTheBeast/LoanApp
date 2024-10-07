using LoanApp.Web.Models;
using LoanApp.Web.Service.IService;
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

        public IActionResult Index()
        {
            var model = new LoanRequest
            {
                LoanAmount = 10000 // Set a default or previously submitted value
            };
            return View(model);
        }
        public async Task<IActionResult> LoanRequest(LoanRequest loanRequest)
        {
            try
            {
                var response = await _loanService.CreateLoanRequest(loanRequest);

                if (response.IsSucess)
                {
                    TempData["success"] = response.Result;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = response.Message;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
