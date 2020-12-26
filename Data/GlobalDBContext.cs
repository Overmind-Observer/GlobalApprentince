
using System;
using System.IO;
using Global_Intern.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Global_Intern.Data
{
    public class GlobalDBContext : DbContext
    {
        public ConnectionString _connectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = GeneratePath();

           /* _ = optionsBuilder.UseSqlServer(connectionString,
              Builder);*/

_ = optionsBuilder.UseSqlServer(connectionString,
              builder => builder.UseRowNumberForPaging(true));

        }

        private void Builder(SqlServerDbContextOptionsBuilder obj) //memberBuilder is unused !?
        {
            throw new NotImplementedException();
        }

       /* private void AddDbContext<T>(Func<object, object> p)
        {
            throw new NotImplementedException();
        }*/

        public string GeneratePath()
        {
            System.IO.DirectoryInfo myDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            //string parentDirectory = myDirectory.Parent.FullName;
            //parentDirectory = parentDirectory.Substring(0, parentDirectory.Length);
            //parentDirectory = parentDirectory + "UserDB.mdf";
            string parentDirectory = myDirectory + "\\UserDB.mdf";
            string Firstpart = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=";
            string Lastpart = ";Integrated Security = True;Connect Timeout =30";
            string Fullpath = Firstpart + parentDirectory + Lastpart;
            return Fullpath;
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
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<UserCompany> UserCompany { get; set; }
        public DbSet<UserCL> UserCL { get; set; }
        public DbSet<UserCV> UserCV { get; set; }
        public DbSet<Course> Course { get; set; }

        public DbSet<Document> Documents { get; set; }

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
