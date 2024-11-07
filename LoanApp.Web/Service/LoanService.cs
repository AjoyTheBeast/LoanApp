using LoanApp.Web.Models;
using LoanApp.Web.Service.IService;
using LoanApp.Web.Utility;

namespace LoanApp.Web.Service
{
    public class LoanService : ILoanService
    {
        private readonly IBaseService baseService;

        public LoanService(IBaseService baseService, IConfiguration configuration)
        {
            this.baseService = baseService;
        }
        public async Task<Response?> CreateLoanRequest(LoanRequest loanRequest)
        {
            return await baseService.SendAsync(new Request()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.LoanApiBaseUrl + "api/loan/createLoanRequest",
                Data = loanRequest,
            }, true);
        }
    }
}
