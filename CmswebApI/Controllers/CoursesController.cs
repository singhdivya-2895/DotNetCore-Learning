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
using Microsoft.Extensions.Logging;

namespace CmswebApI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    // [Route("v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsrepository _cmsrepository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICmsrepository cmsrepository,ILogger<CoursesController> logger)
        {
            this._cmsrepository = cmsrepository ?? throw new ArgumentNullException(nameof(cmsrepository));
            _logger = logger;
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
            _logger.LogInformation("GetCourses Started");
            try
            {
                IEnumerable<Course> courses = await _cmsrepository.GetAllCoursesAsync();
                var result = MappingHelper.MapCourseModelListToCourseDtoList(courses);
                return Ok(result.ToList());
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in GetCoursesAsync {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //_logger.LogError($"Error in GetCoursesAsync {ex.Message}");
            }
        }

        [HttpGet("{courseID}")]
        public async Task<ActionResult<CourseDto>> GetCourseByIdAsync(int courseID)
        {
            _logger.LogInformation("GetCourses with ID Started");
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
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in GetCourseByID {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region "Post Routes"
        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourseAsync([FromBody] CourseDto courseDto)
        {
            _logger.LogInformation(" AddCourses Started");
            try
            {
                var courseModel = MappingHelper.MapCourseDtoToCourseModel(courseDto);
                var newCourse = await _cmsrepository.AddCourseAsync(courseModel);
                return Ok(MappingHelper.MapCourseModelToCourseDto(newCourse));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in AddCourse {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region "Put Routes"
        [HttpPut("{courseID}")]
        public async Task<ActionResult<CourseDto>> UpdateCourseAsync(int courseID, CourseDto course)
        {

            _logger.LogInformation(" UpdateCourse Started");
            try
            {
                Course updatedCourseModel = MappingHelper.MapCourseDtoToCourseModel(course);
                updatedCourseModel = await _cmsrepository.UpdateCourseAsync(courseID, updatedCourseModel);

                if (updatedCourseModel == null)
                {
                    return NotFound();
                }

                var result = MappingHelper.MapCourseModelToCourseDto(updatedCourseModel);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in UpdateCourse {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region "Delete Route"
        [HttpDelete("{courseID}")]
        public async Task<ActionResult<CourseDto>> DeleteCourseByIdAsync(int courseID)
        {
            _logger.LogInformation(" DeleteCourse with Id Started");
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

                return Ok($"Course with Id {courseID} deleted successfully.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in DeleteCourse {ex.Message}");
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
            _logger.LogInformation(" To Get Student Started");
            try
            {
                if (!_cmsrepository.IsCourseExists(courseID))
                {
                    return NotFound();
                }
                IEnumerable<Student> studentModelList = _cmsrepository.GetStudent(courseID);
                var result = MappingHelper.MapStudentModelListToStudentDtoList(studentModelList);
                return Ok(result.ToList());
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in GetStudent {ex.Message}");
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
            _logger.LogInformation("AddStudent Started");
            try
            {
                if (!_cmsrepository.IsCourseExists(courseID))
                {
                    return NotFound();
                }
                Student studentModel = MappingHelper.MapStudentDtoToStudentModel(student);
                Student studentAddedModel = _cmsrepository.AddStudent(courseID, studentModel);
                var result = MappingHelper.MapStudentModelToStudentDto(studentAddedModel);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error in AddStudent {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}