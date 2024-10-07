using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoanApp.Services.LoanApi.Models;

public partial class LoanRequest
{
    [Key]
    public int Id { get; set; }

    public string LoanNumber { get; set; } = null!;

    public string? ApplicantName { get; set; }

    public string? Address { get; set; }

    public int? CreditScore { get; set; }

    public int LoanAmount { get; set; }

    public int ApplicantId { get; set; }
    public string? Email { get; set; }
    public double AnnualAmount { get; set; }
}
