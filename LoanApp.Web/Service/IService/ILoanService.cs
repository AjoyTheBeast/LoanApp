using LoanApp.Web.Models;

namespace LoanApp.Web.Service.IService
{
    public interface ILoanService
    {
        Task<Response?> CreateLoanRequest(LoanRequest loanRequest);
    }
}
