using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.DTOs;
using CmswebApI.Repository.Models;
using CmswebApI.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CmswebApI.Mapping;

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
        // [HttpGet]
        // public IEnumerable<CourseDto> GetCourses()
        // {
        //     try
        //     {
        //         IEnumerable<Course> courses = _cmsrepository.GetAllCourses();
        //         var result = MapCourseToCourseDto(courses);
        //         return result;
        //     }
        //     catch (System.Exception)
        //     {

        //         throw;
        //     }
        // }
        //  Approach 2 Using IActionResult
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesAsync()
        {
            try
            {
                IEnumerable<Course> courses = await _cmsrepository.GetAllCoursesAsync();
                var result = MappingHelper.MapCourseModelListToCourseDtoList(courses);
                return result.ToList();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<CourseDto> AddCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                var courseModel = MappingHelper.MapCourseDtoToCourseModel(courseDto);
                var newCourse = _cmsrepository.AddCourse(courseModel);
                return MappingHelper.MapCourseModelToCourseDto(newCourse);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{courseID}")]
        public ActionResult<CourseDto> GetCourse(int courseID)
        {
            try
            {
                if (!_cmsrepository.IsCourseExists(courseID))
                    return NotFound();

                Course course = _cmsrepository.GetCourse(courseID);
                var result = MappingHelper.MapCourseModelToCourseDto(course);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}