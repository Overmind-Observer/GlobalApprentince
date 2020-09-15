using System;

namespace Global_Intern.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public int Documentupload { get; set; }
        public DateTime DocumentStartDate { get; set; }
        public virtual User User { get; set; }
    }
}
