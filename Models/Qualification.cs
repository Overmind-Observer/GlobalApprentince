using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }
        public string QualificationSchool { get; set; } // bachelor's,  Masters Phd -> Via a static DropDownList
        public string QualificationTitle { get; set; }
        public string QualificationFieldofStudy { get; set; }
        public string QualificationGrade { get; set; }
        public DateTime QualificationStartDate { get; set; }
        public DateTime QualificationEndDate { get; set; }
        public bool QualificationStillStuding { get; set; }
        public Student Student { get; set; } // StudentId [FK]
    }
}
