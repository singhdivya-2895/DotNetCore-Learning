using System.Collections.Generic;
using System.Linq;
using CmswebApI.DTOs;
using CmswebApI.Repository.Models;
using CmswebApI.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CmswebApI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {

        private readonly ICmsrepository _cmsrepository;
        public CoursesController(ICmsrepository cmsrepository)
        {
            this._cmsrepository = cmsrepository;
        }
        //Approach 1
        // [HttpGet]
        // public IEnumerable<Course> GetCourses()
        // {
        //     return _cmsrepository.GetAllCourses();
        // }
        [HttpGet]
        public IEnumerable<CourseDto> GetCourses()
        {
            try
            {
                IEnumerable<Course> courses = _cmsrepository.GetAllCourses();
                var result = MapCourseToCourseDto(courses);
                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //Custom mapper functions
        private CourseDto MapCourseToCourseDto(Course course)
        {
            return new CourseDto()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDuration = course.CourseDuration,
                CourseType = (CmswebApI.DTOs.COURSE_TYPE)course.CourseType
            };
        }
        private IEnumerable<CourseDto> MapCourseToCourseDto(IEnumerable<Course> courses)
        {
            IEnumerable<CourseDto> result;

            result = courses.Select(c => new CourseDto()
            {
                CourseID = c.CourseID,
                CourseName = c.CourseName,
                CourseDuration = c.CourseDuration,
                CourseType = (CmswebApI.DTOs.COURSE_TYPE)c.CourseType
            });
            return result;
        }
    }
}