namespace LoanApp.Web.Utility
{
    public class SD
    {
        public static string LoanApiBaseUrl { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
