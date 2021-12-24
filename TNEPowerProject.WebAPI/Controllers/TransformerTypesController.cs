using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.Interfaces;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformerTypesController : ControllerBase, ITransformerTypesAPI
    {
        private readonly EnergoDBContext energoContext;

        public TransformerTypesController(EnergoDBContext energoContext)
        {
            this.energoContext = energoContext;
        }

        [HttpGet("list")]
        public Task<TransformerTypesListDTO> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
