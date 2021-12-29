using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityConsumptionObjectsController : ControllerBase, IElectricityConsumptionObjectsAPI
    {
        private readonly IElectricityConsumptionObjectsService electricityConsumptionObjectsService;
        public ElectricityConsumptionObjectsController(IElectricityConsumptionObjectsService electricityConsumptionObjectsService)
        {
            this.electricityConsumptionObjectsService = electricityConsumptionObjectsService;
        }
        [HttpGet]
        public async Task<TNEBaseDTO<ElectricityConsumptionObjectsListDTO>> GetAll()
        {
            return await electricityConsumptionObjectsService.GetAllElectricityConsumptionObjects();
        }
    }
}
