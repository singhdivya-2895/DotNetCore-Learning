using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Data.Repository.Models;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public interface ICmsrepository
    {
        IEnumerable<Course> GetAllCourses();
        Task<IEnumerable<Course>> AddCoursesAsync();
        Course AddCourse(Course newCourse);
        Task<Course> AddCourseAsync(Course newCourse);
        bool IsCourseExists(int courseID);
        Course GetCourseById(int courseID);
        Task<bool> IsCourseExistsAsync(int courseID);
        Task<Course> GetCourseByIdAsync(int courseID);
        Task<Course> UpdateCourseAsync(int courseID , Course Newcourse);
        Task<bool> DeleteCourseByIdAsync(int courseID);
        //Assoiation  
         IEnumerable<Student> GetStudent(int courseId);
         Student AddStudent(int courseID,Student student);
         
    }
}