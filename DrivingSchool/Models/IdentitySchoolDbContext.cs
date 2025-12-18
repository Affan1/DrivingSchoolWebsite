using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Models
{
    public class IdentitySchoolDbContext : IdentityDbContext<Users>
    {
        public IdentitySchoolDbContext(DbContextOptions<IdentitySchoolDbContext> options) : base(options)
        {
        }
    }
}
