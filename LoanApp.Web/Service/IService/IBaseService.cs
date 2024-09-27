using LoanApp.Web.Models;

namespace LoanApp.Web.Service.IService
{
    public interface IBaseService
    {
        Task<Response> SendAsync(Request request);
    }
}
