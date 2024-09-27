namespace LoanApp.Services.LoanApi.Models.DTO
{
    public class LoanRequestDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }
        public int LoanAmount { get; set; }
        public int ApplicantId { get; set; }
    }
}
