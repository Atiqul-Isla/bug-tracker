using Microsoft.EntityFrameworkCore;
using bug_tracker_web.Models;
using System.Transactions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

namespace bug_tracker_web.Models
{
    public class ApplicationDbContext : IdentityDbContext<DefaultUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Builder project to user relationship
            builder.Entity<ProjectUser>().HasKey(pu => new 
            {
                pu.ProjectId,
                pu.UserId
            });

            // Builder bug to user relationship
            builder.Entity<BugUser>().HasKey(bu => new
            {
                bu.BugId,
                bu.UserId
            });

            builder.Entity<Project>().HasMany(p => p.Bugs).WithOne(b => b.Project).OnDelete(DeleteBehavior.Cascade);

            // Foreign Key soecifics project to user
            builder.Entity<ProjectUser>().HasOne(p => p.Project).WithMany(pu => pu.ProjectUsers).HasForeignKey(p => p.ProjectId);
            builder.Entity<ProjectUser>().HasOne(p => p.User).WithMany(pu => pu.ProjectUsers).HasForeignKey(p => p.UserId);

            // Foreign Key soecifics project to user
            builder.Entity<BugUser>().HasOne(b => b.Bug).WithMany(bu => bu.BugUsers).HasForeignKey(b => b.BugId);
            builder.Entity<BugUser>().HasOne(b => b.User).WithMany(bu => bu.BugUsers).HasForeignKey(b => b.UserId);

            base.OnModelCreating(builder);
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<DefaultUser> Users { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; } // Add this line

    }
}
