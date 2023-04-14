using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmswebApI.Repository.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public int CourseDuration { get; set; }
        public COURSE_TYPE CourseType { get; set; }
    }
    public enum COURSE_TYPE
    {
        Engineering,
        Medical,
        Management
    }
}