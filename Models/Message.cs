using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class Message
    {
        
        public int MessageId { get; set; }
        public DateTime MessageDate { get; set; }
        // Check Dublicate in row for MessageFrom and MessageTo
        public string MessageFrom { get; set; } // make use of email as an Identifier
        public string MessageTo { get; set; } // make use of emailas an Identifier
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
        public bool MessageRead { get; set; }
        public InternStudent InternStudent { get; set; } // InternStudentId
    }
}
