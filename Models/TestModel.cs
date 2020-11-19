using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models
{
    public class TestModel
    {

        [Required]
        public int TestModelId { get; set; }

        [Required]
        public string Test1 { get; set; }

        public string Test2 { get; set; }

        public bool Test3 { get; set; }
    }
}
