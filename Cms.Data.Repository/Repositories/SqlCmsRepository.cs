using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<Course> GetAllCourses()
        {
            return null;
        }

        public Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(int CourseID)
        {
            throw new NotImplementedException();
        }

        public bool IsCourseExists(Course Id)
        {
            throw new NotImplementedException();
        }

        public bool IsCourseExists(int courseID)
        {
            throw new NotImplementedException();
        }
    }
}