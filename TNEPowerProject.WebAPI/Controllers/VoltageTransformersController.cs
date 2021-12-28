using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoltageTransformersController : ControllerBase, IVoltageTransformersAPI
    {
        private readonly IVoltageTransformersService voltageTransformersService;

        public VoltageTransformersController(IVoltageTransformersService voltageTransformersService)
        {
            this.voltageTransformersService = voltageTransformersService;
        }
        [HttpGet("{voltageTransformerId}/exists")]
        public async Task<TNEBaseDTO<VoltageTransformerExistenceDTO>> CheckVoltageTransformerExists(int voltageTransformerId)
        {
            return await voltageTransformersService.CheckVoltageTransformerExists(voltageTransformerId);
        }
        [HttpPost]
        public async Task<TNEBaseDTO<VoltageTransformerDTO>> CreateVoltageTransformer(CreateVoltageTransformerDTO createVoltageTransformerDTO)
        {
            return await voltageTransformersService.CreateVoltageTransformer(createVoltageTransformerDTO);
        }
    }
}
