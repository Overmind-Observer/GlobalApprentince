using System.ComponentModel.DataAnnotations;

namespace Global_Intern.Models
{
    public class UserDocument
    {
        public int UserDocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentPath { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
