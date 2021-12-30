using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO.ExpiredElectricEquipment;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для получения списков объектов электрической инфраструктуры (трансформаторов и
    /// счётчиков электрической энергии) с истёкшими сроками поверки
    /// </summary>
    public interface IExpiredElectricEquipmentAPI
    {
        /// <summary>
        /// Позволяет получить список всех счётчиков электрической энергии с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        [HttpGet("eemeters")]
        [Get("/api/expiredelectricequipment/eemeters?consObjId={consObjId}")]
        Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>> GetExpiredElectricEnergyMeters(int consObjId);
        /// <summary>
        /// Позволяет получить список всех трансформаторов тока с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        [HttpGet("currenttransformers")]
        [Get("/api/expiredelectricequipment/currenttransformers?consObjId={consObjId}")]
        Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>> GetExpiredCurrentTransformers(int consObjId);
        /// <summary>
        /// Позволяет получить список всех трансформаторов напряжения с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        [HttpGet("voltagetransformers")]
        [Get("/api/expiredelectricequipment/voltagetransformers?consObjId={consObjId}")]
        Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>> GetExpiredVoltageTransformers(int consObjId);
    }
}
