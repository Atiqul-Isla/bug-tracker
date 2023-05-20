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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectUser>().HasKey(pu => new 
            {
                pu.ProjectId,
                pu.UserId
            });

            base.OnModelCreating(builder);

            builder.Entity<ProjectUser>().HasOne(p => p.Project).WithMany(pu => pu.ProjectUsers).HasForeignKey(p => p.ProjectId);
            builder.Entity<ProjectUser>().HasOne(p => p.User).WithMany(pu => pu.ProjectUsers).HasForeignKey(p => p.UserId);

            base.OnModelCreating(builder);
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<DefaultUser> Users { get; set; }
    }
}
