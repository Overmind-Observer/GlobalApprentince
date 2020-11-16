using Global_Intern.Models;
using System.Collections.Generic;
using System.Linq;

namespace Global_Intern.Util
{
    public class UserFilter
    {
        // Created to remove vital info like salt from the user.
        public static List<Internship> RemoveUserInfoFromInternship(List<Internship> intern)
        {

            for (int i = 0; i < intern.Count(); i++)
            {
                intern[i].User.UserPassword = "";
                intern[i].User.Salt = "";
            }

            return intern;
        }

        public static List<Course> RemoveUserInfoFromCourses(List<Course> course)
        {
            for (int i = 0; i < course.Count(); i++)
            {
                course[i].User.UserPassword = "";
                course[i].User.Salt = "";
            }

            return course;
        }
    }
}
