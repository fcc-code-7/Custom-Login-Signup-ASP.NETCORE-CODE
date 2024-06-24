using Altivix.Entities;
using Altivix.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Altivix.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectGallery> ProjectGalleries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define foreign key relationships for Projects
            builder.Entity<Projects>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Projects>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);

            // Define foreign key relationships for ProjectTask
            builder.Entity<ProjectTask>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTasks)
                .HasForeignKey(pt => pt.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            // Define foreign key relationships for ProjectGallery
            builder.Entity<ProjectGallery>()
                .HasOne(pg => pg.Project)
                .WithMany(p => p.ProjectGalleries)
                .HasForeignKey(pg => pg.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
