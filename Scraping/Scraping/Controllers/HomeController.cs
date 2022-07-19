using Microsoft.AspNetCore.Mvc;

namespace Scraping.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Inicio");
        }
    }
}