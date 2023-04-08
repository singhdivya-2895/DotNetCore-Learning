using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CmswebApI.DTOs
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        
        // [Required]
        // [MaxLength(25)]
        public string CourseName { get; set; }
        
        //[Required]
        //[Range(1,5)]
        public int CourseDuration { get; set; }
        public string Source { get; } = "I am from Dto";
        
        //[Required]
        // To fix the enum issue when request comes from Postman. By default Enum looks for integer value
         [JsonConverter(typeof(JsonStringEnumConverter))]
        public COURSE_TYPE CourseType { get; set; }
    }
    public enum COURSE_TYPE
    {
        Engineering,
        Medical,
        Management
    }
}
