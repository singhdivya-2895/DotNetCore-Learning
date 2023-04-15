using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CmswebApI.DTOs
{
    /// <summary>
    /// Course DTO
    /// </summary>
    public class CourseDto
    {
        /// <summary>
        /// Id of the Course
        /// </summary>
        /// <example>1</example>
        public int CourseID { get; set; }

        // [Required]
        // [MaxLength(25)]

        /// <summary>
        /// Name of the Course
        /// </summary>
        /// <example>Computer Science</example>
        public string CourseName { get; set; }

        //[Required]
        //[Range(1,5)]

        /// <summary>
        /// Duration of the Course
        /// </summary>
        /// <example>2</example>
        public int CourseDuration { get; set; }


        /// <summary>
        /// Source of the Data
        /// </summary>
        /// <example>I am from Dto</example>
        public string Source { get; } = "I am from Dto";

        //[Required]

        // To fix the enum issue when request comes from Postman. By default Enum looks for integer value
        /// <summary>
        ///     The type of the course
        /// </summary>
        /// <example>Medical</example>
        public COURSE_TYPE CourseType { get; set; }
    }
    /// <summary>
    /// Course Type List
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum COURSE_TYPE
    {
        Engineering,
        Medical,
        Management
    }
}
