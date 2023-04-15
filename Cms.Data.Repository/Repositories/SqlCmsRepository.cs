using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Repository.Models;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public class SqlCmsRepository : ICmsrepository
    {
        public SqlCmsRepository()
        {
        }

        public Course AddCourse(Course newCourse)
        {
            throw new NotImplementedException();
        }

        public Student AddStudent(int courseId, Student student)
        {
            throw new NotImplementedException();
        }

        public Task<Course> DeleteCourseByIdAsync(int courseID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return null;
        }

        public Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Course GetCourseById(int courseID)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetCourseByIdAsync(int courseID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudent(int courseId)
        {
            throw new NotImplementedException();
        }

        public bool IsCourseExists(int courseID)
        {
            throw new NotImplementedException();
        }

        public bool IsCourseExistsAsync(int courseID)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> UpdateCourseAsync(int courseID, Course Newcourse)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICmsrepository.DeleteCourseByIdAsync(int courseID)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICmsrepository.IsCourseExistsAsync(int courseID)
        {
            throw new NotImplementedException();
        }
    }
}