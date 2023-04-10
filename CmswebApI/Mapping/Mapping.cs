using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.DTOs;
using CmswebApI.Repository.Models;

namespace CmswebApI.Mapping
{
    public static class MappingHelper
    {
        
        //Custom mapper functions
        public static CourseDto MapCourseModelToCourseDto(Course course)
        {
            return new CourseDto()
            {
                CourseID = course.CourseID,
                CourseName = course.CourseName,
                CourseDuration = course.CourseDuration,
                CourseType = (CmswebApI.DTOs.COURSE_TYPE)course.CourseType
            };
        }
        public static IEnumerable<CourseDto> MapCourseModelListToCourseDtoList(IEnumerable<Course> courses)
        {
            IEnumerable<CourseDto> result;

            result = courses.Select(c => MapCourseModelToCourseDto(c));
            return result;
        }
        
        public static Course MapCourseDtoToCourseModel(CourseDto courseDto)
        {
            return new Course()
            {
                CourseID = courseDto.CourseID,
                CourseName = courseDto.CourseName,
                CourseDuration = courseDto.CourseDuration,
                CourseType = (Repository.Models.COURSE_TYPE)courseDto.CourseType
            };
        }
    }
}