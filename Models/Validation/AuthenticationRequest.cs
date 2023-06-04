using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrmAssistant.Models.Validation
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
