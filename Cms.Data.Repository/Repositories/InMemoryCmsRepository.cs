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

        public Course GetCourseById(int courseID)
        {
            var result = courses.Where(c => c.CourseID == courseID)
            .SingleOrDefault();
            return result;
        }
        public bool IsCourseExists(int courseID)
        {
            return courses.Any(c => c.CourseID == courseID);
        }        

        public async Task<Course> GetCourseByIdAsync(int courseID)
        {
            // FirstORDefault:-returns the first element of a sequence that satisfies a specified condition,
            //  or a default value if no such element is found.
            //  If the sequence contains more than one element that satisfies the condition,
            //  it returns the first one. 
            // SingleOrDefault:- returns the only element of a sequence that satisfies a 
            // specified condition, or a default value if no such element is found. 
            // If the sequence contains more than one element that satisfies the condition, it throws an exception.
            var result = courses.Where(c => c.CourseID == courseID)
            .SingleOrDefault();
            return await Task.Run(() => result);
        }
        public async Task<bool> IsCourseExistsAsync(int courseID)
        {
            // Any returns true if the list contains any item with the defined condition, else return false.
            return await Task.Run(() => courses.Any(c => c.CourseID == courseID));
        }
    }
}