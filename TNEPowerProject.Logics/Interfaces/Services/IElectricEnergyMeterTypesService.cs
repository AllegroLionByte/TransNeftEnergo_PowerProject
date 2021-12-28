using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для типов счётчиков электрической энергии
    /// </summary>
    public interface IElectricEnergyMeterTypesService
    {
        /// <summary>
        /// Позволяет добавить новый тип счётчика электрической энергии
        /// </summary>
        Task<TNEBaseDTO<ElectricEnergyMeterTypeDTO>> CreateElectricEnergyMeterType(CreateElectricEnergyMeterTypeDTO createElectricEnergyMeterTypeDTO);
        /// <summary>
        /// Позволяет получить список всех типов счётчиков электрической энергии
        /// </summary>
        Task<TNEBaseDTO<ElectricEnergyMeterTypesListDTO>> GetAllElectricEnergyMeterTypes();
        /// <summary>
        /// Позволяет проверить существование типа счётчика электрической энергии с указанным Id
        /// </summary>
        /// <param name="electricEnergyMeterTypeId">
        /// Id типа счётчика электрической энергии
        /// </param>
        Task<TNEBaseDTO<ElectricEnergyMeterTypeExistenceDTO>> CheckElectricEnergyMeterTypeExists(int electricEnergyMeterTypeId);
    }
}
