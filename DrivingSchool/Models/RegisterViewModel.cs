using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = String.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}
