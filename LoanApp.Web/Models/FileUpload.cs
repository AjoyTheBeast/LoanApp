using LoanApp.Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace LoanApp.Web.Models
{
    public class FileUpload
    {
        public int RequestId { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? File { get; set; }
    }
}
