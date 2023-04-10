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
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.Run(() => courses.ToList());
        }

        public Course AddCourse(Course newCourse)
        {
            var maxCourseID = courses.Max(c => c.CourseID);
            newCourse.CourseID = maxCourseID + 1;
            // Add Course in Database
            courses.Add(newCourse);
            return newCourse;
        }

        public Course GetCourse(int courseID)
        {
            var result = courses.Where(c => c.CourseID == courseID)
            .SingleOrDefault();
            return result;
        }
        public bool IsCourseExists(int courseID)
        {
            return courses.Any(c => c.CourseID == courseID);
        }
    }
}