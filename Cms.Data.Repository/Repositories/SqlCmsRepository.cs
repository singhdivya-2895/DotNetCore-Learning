using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cms.Data.Repository.Data;
using Cms.Data.Repository.Models;
using CmswebApI.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CmswebApI.Repository.Repositories
{
    public class SqlCmsRepository : ICmsrepository
    {
        private readonly CollegeDbContext _dbContext;
        public SqlCmsRepository(CollegeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course AddCourse(Course newCourse)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> AddCourseAsync(Course newCourse)
        {
           await _dbContext.CourseList.AddAsync(newCourse);
           await _dbContext.SaveChangesAsync();
            return newCourse;
        }

        public Student AddStudent(int courseId, Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCourseByIdAsync(int courseID)
        {
            var courseToDelete = _dbContext.CourseList.FirstOrDefault(x => x.CourseID == courseID);
            if (courseToDelete == null)
            {
                return false;
            }
            _dbContext.CourseList.Remove(courseToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _dbContext.CourseList.ToListAsync();
        }

        public Course GetCourseById(int courseID)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> GetCourseByIdAsync(int courseID)
        {
           var getCourseById =await _dbContext.CourseList.FirstOrDefaultAsync(x => x.CourseID==courseID);
            if (getCourseById == null)
            {
              return null;
            }
            return getCourseById;
        }

        public IEnumerable<Student> GetStudent(int courseId)
        {
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
            var courseToUpdate = await _dbContext.CourseList.FirstOrDefaultAsync(x => x.CourseID == courseID);
            if (courseToUpdate == null)
            {
                return null;
            }
            courseToUpdate.CourseName = newCourse.CourseName;
            courseToUpdate.CourseDuration = newCourse.CourseDuration;
            courseToUpdate.CourseType = newCourse.CourseType;

            await _dbContext.SaveChangesAsync();
            return courseToUpdate;
        }
    }
}