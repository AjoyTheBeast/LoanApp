using Microsoft.AspNetCore.Mvc;

namespace LoanApp.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
