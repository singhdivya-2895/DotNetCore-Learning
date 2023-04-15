using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmswebApI.DTOs
{
    /// <summary>
    ///      Student DTO
    /// </summary>
    public class StudentDto
    {
        public int StudentId { get; set; }
        /// <summary>
        ///  First Name of the student
        /// </summary>
        /// <value>Divya</value>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CourseName {get; set;}
    }
}