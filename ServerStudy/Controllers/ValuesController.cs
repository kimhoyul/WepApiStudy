using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace ServerStudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public ContentResult Get()
        {
            string msg = "<span style='color:red;'> Test1 </span>";

            return Content(msg, "text/html");
        }
    }
}