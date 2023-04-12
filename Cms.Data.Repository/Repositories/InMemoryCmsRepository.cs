using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsrepository
    {
        List<Course> courseList = null;
        public InMemoryCmsRepository()
        {
            courseList = new List<Course>();
            courseList.Add
            (
              new Course()
              {
                  CourseID = 1,
                  CourseName = "Computer Science",
                  CourseDuration = 4,
                  CourseType = COURSE_TYPE.Engineering
              }
            );
            courseList.Add
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
            return courseList;
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.Run(() => courseList.ToList());
        }

        public Course AddCourse(Course newCourse)
        {
            var maxCourseID = courseList.Max(c => c.CourseID);
            newCourse.CourseID = maxCourseID + 1;
            // Add Course in Database in future
            courseList.Add(newCourse);
            return newCourse;
        }

        public Course GetCourseById(int courseID)
        {
            var result = courseList.Where(c => c.CourseID == courseID)
                        .SingleOrDefault();
            return result;
        }
        public bool IsCourseExists(int courseID)
        {
            return courseList.Any(c => c.CourseID == courseID);
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
            var result = courseList.Where(c => c.CourseID == courseID)
                        .SingleOrDefault();
            return await Task.Run(() => result);
        }
        public async Task<bool> IsCourseExistsAsync(int courseID)
        {
            // Any returns true if the list contains any item with the defined condition, else return false.
            return await Task.Run(() => courseList.Any(c => c.CourseID == courseID));
        }
        public async Task<Course> UpdateCourseAsync(int courseID, Course Updatedcourse)
        {
            var result = await GetCourseByIdAsync(courseID);

            if(result != null) // Null when the course with this id wont exist
            {
              result.CourseName = Updatedcourse.CourseName;
              result.CourseDuration = Updatedcourse.CourseDuration;
              result.CourseType = Updatedcourse.CourseType;
            }
            // Normally you will send the updates back to source i.e. Database
            return result;
        }

        public  async Task<Course> DeleteCourseByIdAsync(int courseID)
        {
            var result = courseList.Where(c => c.CourseID == courseID)
                        .SingleOrDefault();
                  if (result != null)
                  {
                    courseList.Remove(result);
                  }      
            return await Task.Run(() => result);
        }
    }
}