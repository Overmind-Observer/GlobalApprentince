using System;

namespace Global_Intern.Models
{
    public class Document
    {
        public Document()
        {

        }

        public int DocumentId { get; set; }
        public string UserCVName { get; set; }
        public string UserCLName { get; set; }
        public virtual User User { get; set; }
    }
}
