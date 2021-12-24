using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.Interfaces;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Logics.Interfaces.Services;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformerTypesController : ControllerBase, ITransformerTypesAPI
    {
        private readonly ITransformerTypesService transformerTypesService;

        public TransformerTypesController(ITransformerTypesService transformerTypesService)
        {
            this.transformerTypesService = transformerTypesService;
        }
        [HttpPost]
        public async Task<TransformerTypeDTO> CreateTransformerType(CreateTransformerTypeDTO createTransformerTypeDTO)
        {
            return await transformerTypesService.CreateTransformerType(createTransformerTypeDTO);
        }
        [HttpGet("exists")]
        public async Task<TransformerTypeExistenceDTO> CheckTransformerTypeExists(int transformerTypeId)
        {
            return await transformerTypesService.CheckTransformerTypeExists(transformerTypeId);
        }
        [HttpGet("list")]
        public async Task<TransformerTypesListDTO> GetAll()
        {
            return await transformerTypesService.GetAllTransformerTypes();
        }
    }
}
