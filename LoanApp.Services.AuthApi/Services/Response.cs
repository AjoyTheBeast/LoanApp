namespace LoanApp.Services.AuthApi.Services
{
    public class Response
    {
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;

        public string? Message { get; set; }
    }
}
