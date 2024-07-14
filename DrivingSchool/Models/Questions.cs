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
        public string? FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Message { get; set; }
        public DateTime? Created {  get; set; }
    }
}
