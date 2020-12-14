using System;
using System.ComponentModel.DataAnnotations;
using Global_Intern.Models.TeacherModels;

namespace Global_Intern.Models
{

    public class Course

    {
        public int CourseId { get; set; }   // 1
        [Required]
        public string CourseTitle { get; set; }  // Web development
        public string CourseType { get; set; }      // evening class; 1 to 1 class
        public string CourseDuration { get; set; }   // 10 hours; 50 hours
        [Required]
        public string CourseInfo { get; set; }      // course short intro
        public string CourseFees { get; set; }    // web development 20$ per hour; software tester 25$ per hour


        public System.DateTime CourseExpDate { get; set; }
        public System.DateTime CourseCreatedAt { get; set; }
        public System.DateTime CourseUpdatedAt { get; set; }
        
        // add on 6th 10 2020
        public virtual User User { get; set; }



        public void CreateNewCourse(Course NewCourse, User user)
        {
            this.CourseTitle = NewCourse.CourseTitle;
            this.CourseType = NewCourse.CourseType;
            this.CourseDuration = NewCourse.CourseDuration;
            this.CourseInfo = NewCourse.CourseInfo;
            this.CourseFees = NewCourse.CourseFees;
            this.CourseExpDate = DateTime.UtcNow;
            this.CourseUpdatedAt = DateTime.UtcNow;
            this.CourseCreatedAt = DateTime.UtcNow;
            this.User = user;
            
        }

        public void SetAddorUpdateCourse(TeacherCourseModel course, User user, bool isUpdate = false, int CourseID = 0)
        {
            this.CourseTitle = course.CourseTitle;
            this.CourseType = course.CourseType;
            this.CourseDuration = course.CourseDuration;
            this.CourseInfo = course.CourseInfo;
            this.CourseFees = course.CourseFees;

            if (isUpdate && CourseID != 0)
            {
                this.CourseId = CourseID;
                this.CourseUpdatedAt = DateTime.Now;
            }
            else
            {
                this.CourseCreatedAt = DateTime.Now;
            }

            this.User = user;
        }


    }
}

