using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentTransformersController : ControllerBase, ICurrentTransformersAPI
    {
        private readonly ICurrentTransformersService currentTransformersService;
        public CurrentTransformersController(ICurrentTransformersService currentTransformersService)
        {
            this.currentTransformersService = currentTransformersService;
        }
        [HttpGet("{currentTransformerId}/exists")]
        public async Task<TNEBaseDTO<CurrentTransformerExistenceDTO>> CheckCurrentTransformerExists(int currentTransformerId)
        {
            return await currentTransformersService.CheckCurrentTransformerExists(currentTransformerId);
        }
        [HttpPost]
        public async Task<TNEBaseDTO<CurrentTransformerDTO>> CreateCurrentTransformer(CreateCurrentTransformerDTO createCurrentTransformerDTO)
        {
            return await currentTransformersService.CreateCurrentTransformer(createCurrentTransformerDTO);
        }
    }
}
