using Microsoft.AspNetCore.Http;

namespace Global_Intern.Models.StudentModels
{
    public class ApplyInternship
    {
        public bool isCVExisting { get; set; } // CV (Resume)
        public bool isCLExisting { get; set; } // Cover letter

        public bool isCLTextArea { get; set; } // if cover is from TextArea
        public int? UserCV { get; set; } // get Document ID
        public int? UserCL { get; set; } // get Document ID
        public IFormFile TemporaryCV { get; set; } // get temp path
        public IFormFile TemporaryCL { get; set; } // Cover letter Text
        public string WrittenCL { get; set; } // Cover letter Text
    }
}
