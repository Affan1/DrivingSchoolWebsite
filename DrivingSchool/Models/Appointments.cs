using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Models
{
    public class Appointments
    {
        [Required]
        [Key]
        public int AppointmentID { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string? FullName { get; set; }
        [Required]

        public string? Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? PostalCode { get; set; }
        public bool Agreement { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Datetime { get; set; } = DateTime.Now;
        [MaxLength(500)]
        public string? Description { get; set; }

    }
}
