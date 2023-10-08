using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Cms.Data.Repository.Models;
using CmswebApI.Repository.Models;
using Microsoft.Extensions.Logging;

namespace CmswebApI.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsrepository
    {
        List<Course> courseList = null;
        List<Student> studentList = null;
        private readonly ILogger _logger;

        public InMemoryCmsRepository(ILogger<InMemoryCmsRepository> logger)
        {
            courseList = new List<Course>(){
              new ()
              {
                  CourseID = 1,
                  CourseName = "Computer Science",
                  CourseDuration = 4,
                  CourseType = COURSE_TYPE.Engineering
              },
             new ()
             {
                 CourseID = 2,
                 CourseName = "Information Technology",
                 CourseDuration = 4,
                 CourseType = COURSE_TYPE.Engineering
             }
            };

            studentList = new List<Student>(){
                new Student()
              {
                  StudentId = 100,
                  FirstName = "Divya",
                  LastName = "Chaudhary",
                  PhoneNumber = "0451601867",
                  Address = "Australia",
                  course = courseList.Where(c => c.CourseID == 1).SingleOrDefault()
              },
              new Student()
              {
                  StudentId = 105,
                  FirstName = "Mini",
                  LastName = "Chaudhary",
                  PhoneNumber = "0000000000",
                  Address = "India",
                  course = courseList.Where(c => c.CourseID == 1).SingleOrDefault()
              },
              new Student()
             {
                 StudentId = 101,
                 FirstName = "Pankaj",
                 LastName = "Chaudhary",
                 PhoneNumber = "0451601867",
                 Address = "US",
                 course = courseList.Where(c => c.CourseID == 2).SingleOrDefault()
             }
            };
            _logger = logger;
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
            _logger.LogInformation($"Request to add a new Course Started");
            var maxCourseID = courseList.Max(c => c.CourseID);
            newCourse.CourseID = maxCourseID + 1;
            // Add Course in Database in future
            courseList.Add(newCourse);
            _logger.LogInformation($"Request to add a new Course Finished: { JsonSerializer.Serialize(newCourse) }");
            return newCourse;
        }

        public Course GetCourseById(int courseID)
        {
            _logger.LogInformation($"Request to get a Course with Id:{courseID} started.");
            var result = courseList.SingleOrDefault(c => c.CourseID == courseID);
            _logger.LogInformation($"Request to get a Course with Id:{courseID} finished.");
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
            var result = courseList.SingleOrDefault(c => c.CourseID == courseID);
            return await Task.Run(() => result);
        }
        public async Task<bool> IsCourseExistsAsync(int courseID)
        {
            // Any returns true if the list contains any item with the defined condition, else return false.
            return await Task.Run(() => courseList.Any(c => c.CourseID == courseID));
        }
        public async Task<Course> UpdateCourseAsync(int courseID, Course Updatedcourse)
        {
            _logger.LogInformation($"Request to Update a Course  started.");
            var result = await GetCourseByIdAsync(courseID);

            if (result != null) // Null when the course with this id wont exist
            {
                result.CourseName = Updatedcourse.CourseName;
                result.CourseDuration = Updatedcourse.CourseDuration;
                result.CourseType = Updatedcourse.CourseType;
            }
            // Normally you will send the updates back to source i.e. Database
            _logger.LogInformation($"Request to Update a Course finished.");
            return result;
        }
             
        public async Task<bool> DeleteCourseByIdAsync(int courseID)
        {
            _logger.LogInformation($"Request to Delete a Course  started.");
            var result = courseList.SingleOrDefault(c => c.CourseID == courseID);
            if (result == null)
            {
                return false;
            }
            courseList.Remove(result);
            _logger.LogInformation($"Request to Delete a Course finished.");
            return true;
        }

        public IEnumerable<Student> GetStudent(int courseId)
        {
            return studentList.Where(s => s.course.CourseID == courseId);
        }

        public Student AddStudent(int courseID, Student newStudent)
        {
            _logger.LogInformation($"Request to Add Student Started");
            var maxStudentId = studentList.Max(c => c.StudentId);
            newStudent.StudentId = maxStudentId + 1;
            Course courseToAdd = GetCourseById(courseID);
            newStudent.course = courseToAdd;
            studentList.Add(newStudent);
            _logger.LogInformation($"Request to Add Student finished");
            return newStudent;
        }

        public async Task<Course> AddCourseAsync(Course newCourse)
        {
            _logger.LogInformation($"Request to Add Course Started");
            var maxCourseID = courseList.Max(c => c.CourseID);
            newCourse.CourseID = maxCourseID + 1;
            // Add Course in Database in future
            courseList.Add(newCourse);
            _logger.LogInformation($"Request to Add Course Finished");
            return await Task.Run(() => newCourse); 
        }
    }
}

