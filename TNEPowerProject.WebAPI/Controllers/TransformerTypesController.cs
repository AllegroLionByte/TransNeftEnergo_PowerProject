using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TNEPowerProject.Infrastructure.Database.EFCore;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformerTypesController : ControllerBase
    {
        private readonly EnergoDBContext energoContext;

        public TransformerTypesController(EnergoDBContext energoContext)
        {
            this.energoContext = energoContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(energoContext.TransformerTypes.ToList());
        }

        [HttpGet("v2")]
        public IActionResult Getv2()
        {
            return Ok(energoContext.TransformerTypes.ToList());
        }
    }
}
