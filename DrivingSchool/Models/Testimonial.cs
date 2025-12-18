using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchool.Models
{
    public class Testimonial
    {
        [Required]
        [Key]
        public int TestimonialId { get; set; }
        [Required]
        public string? StudentName { get; set; }
        [Required]
        public string? FeedBackMessage {  get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
