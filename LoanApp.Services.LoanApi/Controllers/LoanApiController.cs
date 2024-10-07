using LoanApp.Services.LoanApi.Models;
using LoanApp.Services.LoanApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LoanApp.Services.LoanApi.Controllers
{
    [Route("api/loan/")]
    [ApiController]
    public class LoanApiController : ControllerBase
    {
        private readonly Response response;
        private readonly AppDbContext _dbContext;
        public LoanApiController(AppDbContext dbContext)
        {
            response = new Response();
            _dbContext = dbContext;
        }
        [HttpPost("createLoanRequest")]
        public async Task<Response> CreateLoanRequest([FromBody]LoanRequestDTO loanRequestDTO)
        {
            try
            {
                LoanRequest request = new LoanRequest()
                {
                    LoanNumber = new Random().Next(1000, 9999).ToString(),
                    ApplicantName = loanRequestDTO.Name,
                    Address = loanRequestDTO.Address,
                    CreditScore = loanRequestDTO.CreditScore,
                    LoanAmount = loanRequestDTO.LoanAmount,
                    ApplicantId = loanRequestDTO.ApplicantId,
                    Email = loanRequestDTO.Email,
                    AnnualAmount = loanRequestDTO.AnnualAmount
                };
                _dbContext.LoanRequests.Add(request);
                await _dbContext.SaveChangesAsync();

                response.IsSucess = true;
                response.Result = "Loan Submitted Successfully";
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
