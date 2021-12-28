using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricEnergyMetersController : ControllerBase, IElectricEnergyMetersAPI
    {
        private readonly IElectricEnergyMetersService electricEnergyMetersService;
        public ElectricEnergyMetersController(IElectricEnergyMetersService electricEnergyMetersService)
        {
            this.electricEnergyMetersService = electricEnergyMetersService;
        }
        [HttpGet("{electricEnergyMeterId}/exists")]
        public async Task<TNEBaseDTO<ElectricEnergyMeterExistenceDTO>> CheckElectricEnergyMeterExists(int electricEnergyMeterId)
        {
            return await electricEnergyMetersService.CheckElectricEnergyMeterExists(electricEnergyMeterId);
        }
        [HttpPost]
        public async Task<TNEBaseDTO<ElectricEnergyMeterDTO>> CreateElectricEnergyMeter(CreateElectricEnergyMeterDTO createElectricEnergyMeterDTO)
        {
            return await electricEnergyMetersService.CreateElectricEnergyMeter(createElectricEnergyMeterDTO);
        }
        [HttpGet]
        public async Task<TNEBaseDTO<ElectricEnergyMetersListDTO>> GetAll()
        {
            return await electricEnergyMetersService.GetAllElectricEnergyMeters();
        }
    }
}
