
using Global_Intern.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Global_Intern.Data
{
    public class GlobalDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _ = optionsBuilder
                .UseSqlServer(connectionString, builder => builder.UseRowNumberForPaging(true));

        }
        public System.Data.Entity.DbSet<Admin> Admins { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<Role> Roles { get; set; }
        public System.Data.Entity.DbSet<Internship> Internships { get; set; }
        public System.Data.Entity.DbSet<AppliedInternship> AppliedInternships { get; set; }
        public System.Data.Entity.DbSet<InternStudent> InternStudents { get; set; }
        public System.Data.Entity.DbSet<Message> Messages { get; set; }
        public System.Data.Entity.DbSet<Qualification> Qualifications { get; set; }
        public System.Data.Entity.DbSet<Experience> Experiences { get; set; }
        public System.Data.Entity.DbSet<Profile> Profiles { get; set; }
        public System.Data.Entity.DbSet<UserDocument> UserDocuments { get; set; }

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
