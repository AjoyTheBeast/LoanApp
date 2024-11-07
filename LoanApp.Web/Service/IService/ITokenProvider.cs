namespace LoanApp.Web.Service.IService
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string GetToken();
    }
}
