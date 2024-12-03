using LoanApp.Services.LoanApi.Models.DTO;

namespace LoanApp.Services.LoanApi.Services.IService
{
    public interface IAzureFunctionService
    {
        Task<bool> ValidateLoanDetails(LoanRequestDTO loanRequestDTO);
    }
}
