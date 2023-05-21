using System;
using System.ComponentModel.DataAnnotations;

namespace Expedia.API.Dtos
{
	public class RegisterDto
	{
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword is not matched")]
        public string ConfirmPassword { get; set; }
    }
}

