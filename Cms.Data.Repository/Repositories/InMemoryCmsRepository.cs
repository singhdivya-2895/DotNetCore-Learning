using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsrepository
    {
        List<Course> courses = null;
        public InMemoryCmsRepository()
        {
            courses = new List<Course>();
            courses.Add
            (
              new Course()
              {
                CourseID = 1,
                CourseName = "Computer Science",
                CourseDuration = 4,
                CourseType = COURSE_TYPE.Engineering
              }             
            );
             courses.Add
            (
              new Course()
              {
                CourseID = 2,
                CourseName = "Information Technology",
                CourseDuration = 4,
                CourseType = COURSE_TYPE.Engineering
              }             
            );
        }
        public IEnumerable<Course> GetAllCourses()
        {
            return courses;
        }
    }
}