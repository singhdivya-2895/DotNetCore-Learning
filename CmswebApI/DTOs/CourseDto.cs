using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmswebApI.DTOs
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseDuration { get; set; }

        public string Source { get; } = "I am from Dto";

        public COURSE_TYPE CourseType { get; set; }
    }

    public enum COURSE_TYPE
    {
        Engineering,
        Medical,
        Management
    }
}