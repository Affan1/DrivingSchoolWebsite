using System.ComponentModel.DataAnnotations;

namespace YourNamespace.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
