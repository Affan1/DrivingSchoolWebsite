using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Appointments> Appointment { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
    }
}
