using System.ComponentModel.DataAnnotations;

namespace CVBanken.Data.Models.Requests.Auth
{
    public class AuthRequest
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}