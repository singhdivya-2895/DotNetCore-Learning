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
using Cms.Data.Repository.Models;

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
        #region "Get Routes"
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

        #endregion

        #region "Post Routes"
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

        #endregion

        #region "Put Routes"
        [HttpPut("{courseID}")]
        public async Task<ActionResult<CourseDto>> UpdateCourseAsync(int courseID, CourseDto course)
        {
            try
            {
                if (!_cmsrepository.IsCourseExists(courseID))
                {
                    return NotFound();
                }
                Course updatedCourseModel = MappingHelper.MapCourseDtoToCourseModel(course);
                updatedCourseModel = await _cmsrepository.UpdateCourseAsync(courseID, updatedCourseModel);
                var result = MappingHelper.MapCourseModelToCourseDto(updatedCourseModel);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region "Delete Route"
        [HttpDelete("{courseID}")]
        public async Task<ActionResult<CourseDto>> DeleteCourseByIdAsync(int courseID)
        {
            try
            {
                if (!await _cmsrepository.IsCourseExistsAsync(courseID))
                {
                    return BadRequest($"Course with Id {courseID} not found");
                }
                var result = await _cmsrepository.DeleteCourseByIdAsync(courseID);

                if (!result)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
        /// <summary>
        /// Get the student on the base of Course Id
        /// </summary>
        /// <param name="courseID">Course Id</param>
        /// <returns></returns>
        [HttpGet("{courseID}/Student")]
        public ActionResult<IEnumerable<StudentDto>> GetStudent(int courseID)
        {
            try
            {
                if (!_cmsrepository.IsCourseExists(courseID))
                {
                    return NotFound();
                }
                IEnumerable<Student> studentModelList = _cmsrepository.GetStudent(courseID);
                var result = MappingHelper.MapStudentModelListToStudentDtoList(studentModelList);
                return result.ToList();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        /// <summary>
        ///     Add the student to the Course.
        /// </summary>
        /// <param name="courseID">Course Id</param>
        /// <param name="student">Student Object</param>
        /// <returns></returns>
        [HttpPost("{courseID}/Student")]
        public ActionResult<StudentDto> AddStudent(int courseID, StudentDto student)
        {
            try
            {
                if (!_cmsrepository.IsCourseExists(courseID))
                {
                    return NotFound();
                }
                Student studentModel = MappingHelper.MapStudentDtoToStudentModel(student);
                Student studentAddedModel = _cmsrepository.AddStudent(courseID, studentModel);
                var result = MappingHelper.MapStudentModelToStudentDto(studentAddedModel);
                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}