using static LoanApp.Web.Utility.SD;

namespace LoanApp.Web.Models
{
    public class Request
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
