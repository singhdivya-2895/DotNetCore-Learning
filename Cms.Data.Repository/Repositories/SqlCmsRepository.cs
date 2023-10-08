using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cms.Data.Repository.Data;
using Cms.Data.Repository.Models;
using CmswebApI.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CmswebApI.Repository.Repositories
{
    public class SqlCmsRepository : ICmsrepository
    {
        private readonly CollegeDbContext _dbContext;
        private readonly ILogger<SqlCmsRepository> _logger;

        public SqlCmsRepository(CollegeDbContext dbContext, ILogger<SqlCmsRepository> logger)
        {
            _dbContext = dbContext;
           _logger = logger;
        }

        public Course AddCourse(Course newCourse)
        {
            _logger.LogInformation($"To Add course {newCourse}");
            throw new NotImplementedException();
        }

        public async Task<Course> AddCourseAsync(Course newCourse)
        {
            _logger.LogInformation($"To Add course Async {newCourse}  started" );
            await _dbContext.CourseList.AddAsync(newCourse);
           await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"To Add course Async {newCourse} finished");
            return newCourse;
        }

        public Student AddStudent(int courseId, Student student)
        {
            _logger.LogInformation($"To Add Student Async {student}  started");
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCourseByIdAsync(int courseID)
        {
            _logger.LogInformation($"To Delete Course  with ID :{courseID}  started");
            var courseToDelete = _dbContext.CourseList.FirstOrDefault(x => x.CourseID == courseID);
            if (courseToDelete == null)
            {
                return false;
            }
            _dbContext.CourseList.Remove(courseToDelete);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"To Delete Student with ID :{courseID}  finished");
            return true;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            _logger.LogInformation($"To GetAll Courses  started");
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            _logger.LogInformation($"To GetAll Courses  ");
            return await _dbContext.CourseList.ToListAsync();
        }

        public Course GetCourseById(int courseID)
        {
            _logger.LogInformation($"To Get Courses with Id {courseID} ");
            throw new NotImplementedException();
        }

        public async Task<Course> GetCourseByIdAsync(int courseID)
        {
            _logger.LogInformation($"To Get Courses with Id {courseID} started ");
            var getCourseById =await _dbContext.CourseList.FirstOrDefaultAsync(x => x.CourseID==courseID);
            if (getCourseById == null)
            {
              return null;
            }
            _logger.LogInformation($"To Get Courses with Id {courseID} finished ");
            return getCourseById;
        }

        public IEnumerable<Student> GetStudent(int courseId)
        {
            _logger.LogInformation($"To Get Student");
            throw new NotImplementedException();
        }

        public bool IsCourseExists(int courseID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsCourseExistsAsync(int courseID)
        {
            return await _dbContext.CourseList.AnyAsync(x => x.CourseID == courseID);
        }

        public async Task<Course> UpdateCourseAsync(int courseID, Course newCourse)
        {
            _logger.LogInformation($"To Update Courses started ");
            var courseToUpdate = await _dbContext.CourseList.FirstOrDefaultAsync(x => x.CourseID == courseID);
            if (courseToUpdate == null)
            {
                return null;
            }
            courseToUpdate.CourseName = newCourse.CourseName;
            courseToUpdate.CourseDuration = newCourse.CourseDuration;
            courseToUpdate.CourseType = newCourse.CourseType;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"To Update Courses finished ");
            return courseToUpdate;
        }
    }
}