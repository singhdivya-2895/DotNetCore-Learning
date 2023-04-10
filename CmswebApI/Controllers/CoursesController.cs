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
        public async Task<ActionResult<CourseDto>> GetCourseByIdAsync(int courseID)
        {
            try
            {
                // E.g: Id is 5, so Any will return false if 5 id doesn't exist.
                // Not of false = True
                // If executes when the condition inside is true
                // So if IsCourseExistsAsync will return false, Line 86 will be execute and the function will return from there.
                if (!await _cmsrepository.IsCourseExistsAsync(courseID))
                {
                    return NotFound();
                }

                Course course = await _cmsrepository.GetCourseByIdAsync(courseID);
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