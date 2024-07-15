using System.ComponentModel.DataAnnotations;

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
        public string? FeedBack {  get; set; }
        [Required]
        public string? ImageUrl { get; set; }
    }
}
