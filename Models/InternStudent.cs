using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class InternStudent
    {
        public int InternStudentId { get; set; }
        public Student Student { get; set; }
        public Internship Internship { get; set; }
        public List<Message> Messages { get; set; }
    }
}
