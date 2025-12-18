using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } =String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } =String.Empty;

        public bool RememberMe { get; set; }
    }
}
