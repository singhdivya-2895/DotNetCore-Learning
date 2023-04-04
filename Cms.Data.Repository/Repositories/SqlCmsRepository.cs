using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.Repository.Models;

namespace CmswebApI.Repository.Repositories
{
    public class SqlCmsRepository : ICmsrepository
    {
        public SqlCmsRepository()
        {
        }
        public IEnumerable<Course> GetAllCourses()
        {
            return null;
        }
    }
}