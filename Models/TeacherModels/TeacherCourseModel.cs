using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Models.TeacherModels
{
    public class TeacherCourseModel
    {
		public int CourseId { get; set; }   // 1 
        public string CourseTitle { get; set; }  // Web development
        public string CourseLogo { get; set; }
		public string CourseTeacher { get; set; }
		public string CourseType { get; set; }      // evening class; 1 to 1 class
        public string CourseDuration { get; set; }   // 10 hours; 50 hours
        public string CourseInfo { get; set; }     // course short intro
        public string CourseFees { get; set; }    // web development 20$ per hour; software tester 25$ per hour
        public System.DateTime CourseExpDate { get; set; }
        public System.DateTime CourseCreatedAt { get; set; }
        public System.DateTime CourseUpdatedAt { get; set; }


        // add on 6th 10 2020 matching dashboard teacher.
        public TeacherCourseModel()
        {

        }
     
        public TeacherCourseModel(Course course)
        {
            CourseTitle = course.CourseTitle;
            CourseType = course.CourseType;
            CourseDuration = course.CourseDuration;
            CourseFees = course.CourseFees;

        }

    }
}
