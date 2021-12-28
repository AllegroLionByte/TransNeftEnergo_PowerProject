using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TNEPowerProject.Contract.Interfaces;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase, IHeartbeatAPI
    {
        [HttpGet("{checkingString}")]
        public Task<string> Heartbeat(string checkingString)
        {
            return Task.FromResult($"I am alive! Your string: [{checkingString}]");
        }
    }
}
