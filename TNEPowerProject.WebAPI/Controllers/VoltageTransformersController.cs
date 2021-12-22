using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TNEPowerProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoltageTransformersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                nasos = "Infection"
            });
        }
    }
}
