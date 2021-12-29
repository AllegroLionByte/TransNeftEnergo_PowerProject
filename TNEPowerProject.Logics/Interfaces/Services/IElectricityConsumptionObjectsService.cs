using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для объектов потребления
    /// </summary>
    public interface IElectricityConsumptionObjectsService
    {
        /// <summary>
        /// Позволяет получить список всех объектов потребления
        /// </summary>
        Task<TNEBaseDTO<ElectricityConsumptionObjectsListDTO>> GetAllElectricityConsumptionObjects();
        /// <summary>
        /// Позволяет получить список всех счётчиков электрической энергии с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>> GetExpiredElectricEnergyMeters(int consObjId);
        /// <summary>
        /// Позволяет получить список всех трансформаторов тока с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>> GetExpiredCurrentTransformers(int consObjId);
        /// <summary>
        /// Позволяет получить список всех трансформаторов напряжения с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>> GetExpiredVoltageTransformers(int consObjId);
    }
}
