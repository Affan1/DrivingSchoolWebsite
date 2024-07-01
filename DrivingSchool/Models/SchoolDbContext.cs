using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Appointments> Appointment { get; set; }
    }
}
