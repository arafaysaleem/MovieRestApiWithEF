﻿using System.ComponentModel.DataAnnotations;

namespace MovieRestApiWithEF.Core.Requests
{
    // Acts as a DTO for Login endpoint
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
