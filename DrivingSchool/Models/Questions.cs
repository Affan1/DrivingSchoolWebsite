using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Models
{
    public class Questions
    {
        [Required]
        [Key]
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string FullName { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Subject { get; set; } = String.Empty;
        [Required]
        public string Message { get; set; } = String.Empty;
        public DateTime Created {  get; set; } = DateTime.Now;
        [Required]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
