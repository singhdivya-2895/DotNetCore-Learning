using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsrepository
    {
        List<Course> course = null;
        public InMemoryCmsRepository()
        {
            course = new List<Course>();
            course.Add
            (
              new Course()
              {
                  CourseID = 1,
                  CourseName = "Computer Science",
                  CourseDuration = 4,
                  CourseType = COURSE_TYPE.Engineering
              }
            );
            course.Add
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
            return course;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.Run(() => course.ToList());
        }

        public Course AddCourse(Course newCourse)
        {
            var maxCourseID = course.Max(c => c.CourseID);
            newCourse.CourseID = maxCourseID + 1;
            // Add Course in Database
            course.Add(newCourse);
            return newCourse;
        }

        public Course GetCourseById(int courseID)
        {
            var result = course.Where(c => c.CourseID == courseID)
            .SingleOrDefault();
            return result;
        }
        public bool IsCourseExists(int courseID)
        {
            return course.Any(c => c.CourseID == courseID);
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
            var result = course.Where(c => c.CourseID == courseID)
            .SingleOrDefault();
            return await Task.Run(() => result);
        }
        public async Task<bool> IsCourseExistsAsync(int courseID)
        {
            // Any returns true if the list contains any item with the defined condition, else return false.
            return await Task.Run(() => course.Any(c => c.CourseID == courseID));
        }
        public Course UpdateCourse(int courseID, Course Updatedcourse)
        {
            var result = course.Where(c => c.CourseID == courseID)
            .SingleOrDefault();

            if(result != null)
            {
              result.CourseName = Updatedcourse.CourseName;
              result.CourseDuration = Updatedcourse.CourseDuration;
              result.CourseType = Updatedcourse.CourseType;

            }
            return result;
        }
    }
}