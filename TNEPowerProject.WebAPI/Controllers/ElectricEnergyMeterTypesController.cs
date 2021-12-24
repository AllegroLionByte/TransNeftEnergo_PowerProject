using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricEnergyMeterTypesController : ControllerBase, IElectricEnergyMeterTypesAPI
    {
        private readonly IElectricEnergyMeterTypesService electricEnergyMeterTypesService;

        public ElectricEnergyMeterTypesController(IElectricEnergyMeterTypesService electricEnergyMeterTypesService)
        {
            this.electricEnergyMeterTypesService = electricEnergyMeterTypesService;
        }
        [HttpPost]
        public async Task<ElectricEnergyMeterTypeDTO> CreateElectricEnergyMeterType(CreateElectricEnergyMeterTypeDTO createElectricEnergyMeterTypeDTO)
        {
            return await electricEnergyMeterTypesService.CreateElectricEnergyMeterType(createElectricEnergyMeterTypeDTO);
        }
        [HttpGet("exists")]
        public async Task<ElectricEnergyMeterTypeExistenceDTO> CheckElectricEnergyMeterTypeExists(int electricEnergyMeterTypeId)
        {
            return await electricEnergyMeterTypesService.CheckElectricEnergyMeterTypeExists(electricEnergyMeterTypeId);
        }
        [HttpGet("list")]
        public async Task<ElectricEnergyMeterTypesListDTO> GetAll()
        {
            return await electricEnergyMeterTypesService.GetAllElectricEnergyMeterTypes();
        }
    }
}
