using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace bug_tracker_web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
    }
}
