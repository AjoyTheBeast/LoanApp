namespace LoanApp.Web.Models
{
    public class Response
    {
        public Object? Result { get; set; }
        public bool IsSucess { get; set; } = true;
        public string Message { get; set; }
    }
}
