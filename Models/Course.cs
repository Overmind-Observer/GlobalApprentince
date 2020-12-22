using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Global_Intern.Models.TeacherModels;

namespace Global_Intern.Models
{

    // TODO for JAKSON
    // Need to be realted to User. 

    public class Course

    {
        
        public int CourseId { get; set; }   // 1
        
        public string CourseTitle { get; set; }  // Web development
        
        public string CourseType { get; set; }      // evening class; 1 to 1 class
        
        public string CourseDuration { get; set; }   // 10 hours; 50 hours
        
        public string CourseInfo { get; set; }      // course short intro
        public string CourseFees { get; set; }    // web development 20$ per hour; software tester 25$ per hour

        
        public System.DateTime CourseExpDate { get; set; }
        
        public System.DateTime CourseCreatedAt { get; set; }
        
        public System.DateTime CourseUpdatedAt { get; set; }

        [JsonIgnore]
        // add on 6th 10 2020
        public virtual User User { get; set; } 



        public void CreateNewCourse(Course NewCourse, User user)
        {
            DateTime todaysDate = new DateTime();
            var nextMonth = todaysDate.AddMonths(1);
            this.CourseTitle = NewCourse.CourseTitle;
            this.CourseType = NewCourse.CourseType;
            this.CourseDuration = NewCourse.CourseDuration;
            this.CourseInfo = NewCourse.CourseInfo;
            this.CourseFees = NewCourse.CourseFees;
            this.CourseExpDate = nextMonth;
            this.CourseUpdatedAt = DateTime.UtcNow;
            this.CourseCreatedAt = DateTime.UtcNow;
            this.User = user;
            
        }

        public Course UpdateCourse(Course course, Course UpdatedCourse)
        {
            course.CourseTitle = UpdatedCourse.CourseTitle;
            course.CourseType = UpdatedCourse.CourseType;
            course.CourseDuration = UpdatedCourse.CourseDuration;
            course.CourseInfo = UpdatedCourse.CourseInfo;
            course.CourseFees = UpdatedCourse.CourseFees;
            course.CourseUpdatedAt = DateTime.Now;

            return course;
        }

    }
}

