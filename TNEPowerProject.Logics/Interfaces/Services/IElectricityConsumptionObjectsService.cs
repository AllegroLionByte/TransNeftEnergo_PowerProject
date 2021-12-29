using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
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
    }
}
