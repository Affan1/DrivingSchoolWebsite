using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Models
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Appointments> Appointments { get; set; }
    }
}
