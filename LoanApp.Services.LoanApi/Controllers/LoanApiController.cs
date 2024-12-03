using LoanApp.Services.LoanApi.Models;
using LoanApp.Services.LoanApi.Models.DTO;
using LoanApp.Services.LoanApi.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanApp.Services.LoanApi.Controllers
{
    [Route("api/loan/")]
    [ApiController]
    public class LoanApiController : ControllerBase
    {
        private readonly Response response;
        private readonly AppDbContext _dbContext;
        private readonly IAzureFunctionService _functionService;
        public LoanApiController(AppDbContext dbContext, IAzureFunctionService functionService)
        {
            response = new Response();
            _dbContext = dbContext;
            _functionService = functionService;
        }
        [HttpPost("createLoanRequest")]
        [Authorize(Roles ="Customer")]
        public async Task<Response> CreateLoanRequest([FromBody]LoanRequestDTO loanRequestDTO)
        {
            try
            {
                var isValid = await _functionService.ValidateLoanDetails(loanRequestDTO);
                string status = isValid ? "basicDetailsVerified" : "basicDetailsPending";
                LoanRequest request = new LoanRequest()
                {
                    LoanNumber = new Random().Next(1000, 9999).ToString(),
                    ApplicantName = loanRequestDTO.Name,
                    Address = loanRequestDTO.Address,
                    CreditScore = loanRequestDTO.CreditScore,
                    LoanAmount = loanRequestDTO.LoanAmount,
                    ApplicantId = loanRequestDTO.ApplicantId,
                    Email = loanRequestDTO.Email,
                    AnnualAmount = loanRequestDTO.AnnualAmount,
                    Status = status,
                };
                _dbContext.LoanRequests.Add(request);
                await _dbContext.SaveChangesAsync();
                
                if(isValid)
                {
                    var requestId = request.Id;
                    response.Message = "Basic details have been approved by system";
                    response.Result = requestId;
                }
                else
                {
                    response.IsSucess = false;
                    response.Message = "Loan submitted but validation failed. Please correct the details.";
                }
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpGet("getLoanById")]
        public async Task<Response> GetLoanDetailById(string loanId)
        {
            try
            {
                var loanDetail = await _dbContext.LoanRequests.FirstOrDefaultAsync(x => x.LoanNumber == loanId);
                if(loanDetail != null)
                {
                    response.Result = loanDetail;
                }
            }
            catch(Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpGet("getLoanDetails")]
        public async Task<Response> GetLoanDetails()
        {
            try
            {
                var loanDetails = await _dbContext.LoanRequests.ToListAsync();
                if (loanDetails != null)
                {
                    response.Result = loanDetails;
                }
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpPost("uploadDocument")]
        [Authorize(Roles = "Customer")]
        public async Task<Response> UploadDocument(DocumentUploadDTO documentUploadDTO)
        {
            try
            {
                if(documentUploadDTO != null)
                {
                    string fileName = documentUploadDTO.File.FileName;
                    var fileExtension = Path.GetExtension(fileName);
                    var localFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", $"{fileName}{fileExtension}");

                    FileInfo file = new FileInfo(localFilePath);
                    if(file.Exists)
                        file.Delete();

                    using var stream = new FileStream(localFilePath, FileMode.Create);
                    documentUploadDTO.File.CopyTo(stream);

                    var document = new Documents()
                    {
                        RequestId = documentUploadDTO.RequestId,
                        FileName = fileName,
                        FilePath = localFilePath
                    };
                    _dbContext.Documents.Add(document);
                    await _dbContext.SaveChangesAsync();
                    response.Message = "Document saved and uploaded successfully";
                    response.IsSucess = true;
                }
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
