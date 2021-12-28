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
        [HttpGet("{electricEnergyMeterTypeId}/exists")]
        public async Task<TNEBaseDTO<ElectricEnergyMeterTypeExistenceDTO>> CheckElectricEnergyMeterTypeExists(int electricEnergyMeterTypeId)
        {
            return await electricEnergyMeterTypesService.CheckElectricEnergyMeterTypeExists(electricEnergyMeterTypeId);
        }
        [HttpPost]
        public async Task<TNEBaseDTO<ElectricEnergyMeterTypeDTO>> CreateElectricEnergyMeterType(CreateElectricEnergyMeterTypeDTO createElectricEnergyMeterTypeDTO)
        {
            return await electricEnergyMeterTypesService.CreateElectricEnergyMeterType(createElectricEnergyMeterTypeDTO);
        }
        [HttpGet]
        public async Task<TNEBaseDTO<ElectricEnergyMeterTypesListDTO>> GetAll()
        {
            return await electricEnergyMeterTypesService.GetAllElectricEnergyMeterTypes();
        }
    }
}
