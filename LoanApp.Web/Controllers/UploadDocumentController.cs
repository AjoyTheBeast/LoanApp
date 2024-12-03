using LoanApp.Web.Models;
using LoanApp.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Add this for serialization

namespace LoanApp.Web.Controllers
{
    public class UploadDocumentController : Controller
    {
        private readonly ILoanService _loanService;

        public UploadDocumentController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public IActionResult Index()
        {
            var document = new FileUpload();
            if (TempData["RequestId"]!=null)
            {
                document.RequestId = Convert.ToInt32(TempData["RequestId"]);
            }
            return View(document);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> UploadDocument(FileUpload document)
        {
            if(ModelState.IsValid)
            {
                var result = await _loanService.UploadDocument(document);

                if (result.IsSucess)
                {
                    TempData["Success"] = result.Message;
                    return View("Index", document);
                }
                else
                    TempData["Error"] = result.Message;
            }

            return View("Index", document);
        }

    }
}
