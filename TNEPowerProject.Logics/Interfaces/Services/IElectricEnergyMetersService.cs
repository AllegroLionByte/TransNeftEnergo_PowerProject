using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для счётчиков электрической энергии
    /// </summary>
    public interface IElectricEnergyMetersService
    {
        /// <summary>
        /// Позволяет добавить новый счётчик электрической энергии
        /// </summary>
        Task<TNEBaseDTO<ElectricEnergyMeterDTO>> CreateElectricEnergyMeter(CreateElectricEnergyMeterDTO createElectricEnergyMeterDTO);
        /// <summary>
        /// Позволяет получить список всех счётчиков электрической энергии
        /// </summary>
        Task<TNEBaseDTO<ElectricEnergyMetersListDTO>> GetAllElectricEnergyMeters();
        /// <summary>
        /// Позволяет проверить существование счётчика электрической энергии с указанным Id
        /// </summary>
        /// <param name="electricEnergyMeterId">
        /// Id счётчика электрической энергии
        /// </param>
        Task<TNEBaseDTO<ElectricEnergyMeterExistenceDTO>> CheckElectricEnergyMeterExists(int electricEnergyMeterId);
    }
}
