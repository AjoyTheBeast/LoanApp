using Microsoft.AspNetCore.Mvc;

namespace LoanApp.Web.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
