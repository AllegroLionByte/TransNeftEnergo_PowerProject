using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityMeasuringPointsController : ControllerBase, IElectricityMeasuringPointsAPI
    {
        private readonly IElectricityMeasuringPointsService electricityMeasuringPointsService;
        public ElectricityMeasuringPointsController(IElectricityMeasuringPointsService electricityMeasuringPointsService)
        {
            this.electricityMeasuringPointsService = electricityMeasuringPointsService;
        }
        [HttpGet("{eMeasPointId}/exists")]
        public async Task<TNEBaseDTO<ElectricityMeasuringPointExistenceDTO>> CheckElectricityMeasuringPointExists(int eMeasPointId)
        {
            return await electricityMeasuringPointsService.CheckElectricityMeasuringPointExists(eMeasPointId);
        }
        [HttpPost]
        public async Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> CreateElectricityMeasuringPoint([Body] CreateElectricityMeasuringPointDTO createElectricityMeasuringPointDTO)
        {
            return await electricityMeasuringPointsService.CreateElectricityMeasuringPoint(createElectricityMeasuringPointDTO);
        }
        [HttpGet("{eMeasPointId}")]
        public async Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> GetElectricityMeasuringPoint(int eMeasPointId)
        {
            return await electricityMeasuringPointsService.GetElectricityMeasuringPoint(eMeasPointId);
        }
    }
}
