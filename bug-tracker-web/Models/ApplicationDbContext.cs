using Microsoft.EntityFrameworkCore;
using bug_tracker_web.Models;
using System.Transactions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace bug_tracker_web.Models
{
    public class ApplicationDbContext : IdentityDbContext<DefaultUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
    }
}
