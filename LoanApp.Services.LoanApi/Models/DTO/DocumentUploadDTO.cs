namespace LoanApp.Services.LoanApi.Models.DTO
{
    public class DocumentUploadDTO
    {
        public int RequestId { get; set; }
        public IFormFile File { get; set; }
    }
}
