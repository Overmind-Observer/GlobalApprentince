using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Global_Intern.Models;

namespace Global_Intern.Data
{
    public class GlobalDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=skynet\sqlexpress;Initial Catalog=GlobalDB;Integrated Security=True");
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
        public DbSet<VisaStatus> VisaStatuses { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
    }
}
