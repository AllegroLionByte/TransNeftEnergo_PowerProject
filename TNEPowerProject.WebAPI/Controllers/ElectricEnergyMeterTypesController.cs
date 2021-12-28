using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes;
using TNEPowerProject.Contract.DTO;

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
        public async Task<TNEBaseDTO<ElectricEnergyMeterTypeDTO>> CreateElectricEnergyMeterType(CreateElectricEnergyMeterTypeDTO createElectricEnergyMeterTypeDTO)
        {
            return await electricEnergyMeterTypesService.CreateElectricEnergyMeterType(createElectricEnergyMeterTypeDTO);
        }
        [HttpGet("exists")]
        public async Task<TNEBaseDTO<ElectricEnergyMeterTypeExistenceDTO>> CheckElectricEnergyMeterTypeExists(int electricEnergyMeterTypeId)
        {
            return await electricEnergyMeterTypesService.CheckElectricEnergyMeterTypeExists(electricEnergyMeterTypeId);
        }
        [HttpGet("list")]
        public async Task<TNEBaseDTO<ElectricEnergyMeterTypesListDTO>> GetAll()
        {
            return await electricEnergyMeterTypesService.GetAllElectricEnergyMeterTypes();
        }
    }
}
