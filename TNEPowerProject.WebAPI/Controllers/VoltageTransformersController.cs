using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.Interfaces;

namespace TNEPowerProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoltageTransformersController : ControllerBase
    {
        private readonly EnergoDBContext energoContext;

        public VoltageTransformersController(EnergoDBContext energoContext)
        {
            this.energoContext = energoContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
