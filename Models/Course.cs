using System;
using System.ComponentModel.DataAnnotations;
using Global_Intern.Models.EmployerModels;



namespace Global_Intern.Models
{

    // TODO for JAKSON
    // Need to be realted to User. 

    public class Course

    {
        public int CourseId { get; set; }   // 1
        [Required]
        public string CourseTitle { get; set; }  // Web development
        public string CourseType { get; set; }      // evening class; 1 to 1 class
        public string CourseDuration { get; set; }   // 10 hours; 50 hours
        [Required]
        public string CourseBody { get; set; }     // course short intro
        public string CourseFees { get; set; }    // web development 20$ per hour; software tester 25$ per hour

     
        public System.DateTime CourseExpDate { get; set; }
        public System.DateTime CourseCreatedAt { get; set; }
        public System.DateTime CourseUpdatedAt { get; set; }

       
    }
}

