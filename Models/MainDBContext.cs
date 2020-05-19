using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class MainDBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<AppliedInternship> AppliedInternships { get; set; }
        public DbSet<InternStudent> InternStudents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }

    }
}
