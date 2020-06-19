
﻿using Global_Intern.Models;
using Microsoft.EntityFrameworkCore;

namespace Global_Intern.Data
{
    public class GlobalDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder

                .UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<AppliedInternship> AppliedInternships { get; set; }
        public DbSet<InternStudent> InternStudents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        

            // User table modification
            modelBuilder.Entity<User>().
                HasIndex(u => u.UserEmail).IsUnique();
            modelBuilder.Entity<User>().Property(b => b.UserEmailVerified).HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(b => b.SoftDelete).HasDefaultValue(false);


        }
    }
}
