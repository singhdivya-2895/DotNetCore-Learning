

using CmswebApI.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CmswebApI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {

        private ICmsrepository cmsrepository;
        public CoursesController(ICmsrepository cmsrepository)
        {
            this.cmsrepository = cmsrepository;
        }
        
        [HttpGet]
        public string GetCourses()
        {
            return "Hello, i am divya!";
        }
    }

}