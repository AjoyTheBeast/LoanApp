﻿using Microsoft.AspNetCore.Identity;

namespace LoanApp.Services.AuthApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
