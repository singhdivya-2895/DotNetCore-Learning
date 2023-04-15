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
        /// <example>Divya</example>
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        
        /// <summary>
        ///  Phone Number of the student
        /// </summary>
        /// <example>000 000 000</example>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        ///  Address of the student
        /// </summary>
        /// <example>Gaon</example>
        public string Address { get; set; }
        
        /// <summary>
        ///  Course Name of the Student
        /// </summary>
        /// <example>Nahi Padhti</example>
        public string CourseName {get; set;}
    }
}