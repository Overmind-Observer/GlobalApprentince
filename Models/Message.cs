using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public virtual User MessageFrom { get; set; }
        public virtual User MessageTo { get; set; }

        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public bool MessageRead { get; set; }

        public virtual InternStudent InternStudent { get; set;}
    }
}
