namespace LoanApp.Web.Models
{
    public class LoanRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }
        public int LoanAmount { get; set; }
        public int ApplicantId { get; set; }
        public string? Email { get; set; }
        public double AnnualAmount { get; set; }
    }
}
