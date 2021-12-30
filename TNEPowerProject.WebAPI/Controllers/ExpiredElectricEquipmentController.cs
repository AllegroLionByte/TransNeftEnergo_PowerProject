using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO.ExpiredElectricEquipment;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpiredElectricEquipmentController : ControllerBase, IExpiredElectricEquipmentAPI
    {
        private readonly IExpiredElectricEquipmentService expiredElectricEquipmentService;
        public ExpiredElectricEquipmentController(IExpiredElectricEquipmentService expiredElectricEquipmentService)
        {
            this.expiredElectricEquipmentService = expiredElectricEquipmentService;
        }
        [HttpGet("eemeters")]
        public async Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>> GetExpiredElectricEnergyMeters(int consObjId)
        {
            return await expiredElectricEquipmentService.GetExpiredElectricEnergyMeters(consObjId);
        }
        [HttpGet("currenttransformers")]
        public async Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>> GetExpiredCurrentTransformers(int consObjId)
        {
            return await expiredElectricEquipmentService.GetExpiredCurrentTransformers(consObjId);
        }
        [HttpGet("voltagetransformers")]
        public async Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>> GetExpiredVoltageTransformers(int consObjId)
        {
            return await expiredElectricEquipmentService.GetExpiredVoltageTransformers(consObjId);
        }
    }
}
