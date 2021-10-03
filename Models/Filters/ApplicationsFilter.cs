using Global_Intern.Models.AdditonalModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.Filters
{
    public class ApplicationsFilter
    {
        public List<AppliedInternship> Applications { get; set; }
        public SelectList Titles { get; set; }
        public SelectList Status { get; set; }
        public string ApplicationTitle { get; set; }
        public string ApplicationStatus { get; set; }
    }
}
