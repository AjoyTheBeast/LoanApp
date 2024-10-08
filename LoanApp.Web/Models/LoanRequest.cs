using System.ComponentModel.DataAnnotations;

namespace LoanApp.Web.Models
{
    public class LoanRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Credit Score is required.")]
        public int CreditScore { get; set; }

        [Required(ErrorMessage = "Identification No is required.")]
        public int ApplicantId { get; set; }

        public int LoanAmount { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Annual Amount is required.")]
        public decimal AnnualAmount { get; set; }
        public string? Status { get; set; }
    }
}