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

        //public string UserDocumnetId { get; set; }
        //public string UserCLFullPath { get; set; }
        //public string UserCVFullPath { get; set; }
        //public DateTime CVCreatedAt { get; set; }
        //public DateTime CLCreatedAt { get; set; }
        //public string CLType { get; set; }

        //public string CVType { get; set; }
        //public string CVTitle { get; set; }

        //public string CLTitle { get; set; }

        //public virtual User User { get; set; }
    }
}
