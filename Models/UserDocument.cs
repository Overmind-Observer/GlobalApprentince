using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models
{
    public class UserDocument
    {
        public int UserDocumentId { get; set; }
        public IFormFile UserCL { get; set; }
        public IFormFile UserCV { get; set; }
        public string UserCVName { get; set; }
        public string UserCLName { get; set; }
        [Required]
        public virtual User User { get; set; }

        public UserDocument()
        {

        }

        public UserDocument(Document document)
        {
            UserCLName = document.UserCLName;

            UserCVName = document.UserCVName;
        }

        public Document CreateOrUpdateDocuments(Document document, UserDocument userDocument, User user)

        {
            if (userDocument.UserCLName != null)
            {
                document.UserCLName = userDocument.UserCLName;
            }

            if (userDocument.UserCVName != null)
            {
                document.UserCVName = userDocument.UserCVName;
            }

            document.User = user;

            return document;
        }


    }
}
