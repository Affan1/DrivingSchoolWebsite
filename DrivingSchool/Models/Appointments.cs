using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Models
{
    public class Appointments
    {
        [Required]
        [Key]
        public int AppointmentID { get; set; }
        [MaxLength(50), MinLength(2)]
        public string? FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? CarType { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateTime { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }

    }
}
