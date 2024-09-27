using System;
using System.Collections.Generic;

namespace LoanApiController.Models;

public partial class LoanRequest
{
    public int Id { get; set; }

    public string LoanNumber { get; set; } = null!;

    public string? ApplicantName { get; set; }

    public string? Address { get; set; }

    public int? CreditScore { get; set; }

    public int LoanAmount { get; set; }

    public int ApplicantId { get; set; }
}
