using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public interface ICmsrepository
    {
        IEnumerable<Course> GetAllCourses();
    }
}