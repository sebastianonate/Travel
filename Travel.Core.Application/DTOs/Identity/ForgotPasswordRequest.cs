using System.ComponentModel.DataAnnotations;

namespace Travel.Core.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}