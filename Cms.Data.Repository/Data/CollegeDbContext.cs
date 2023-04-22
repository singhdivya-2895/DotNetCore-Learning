using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmswebApI.Repository.Models;
using Cms.Data.Repository.Models;

namespace Cms.Data.Repository.Data
{
    public class CollegeDbContext : DbContext
    {
        
        public CollegeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Course> CourseList { get; set; }
        public DbSet<Student> Students { get; set; }
    } 
}
