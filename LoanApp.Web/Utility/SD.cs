namespace LoanApp.Web.Utility
{
    public class SD
    {
        public static string LoanApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl { get; set; }

        public const string TokenCookie = "JwtToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
