using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для счётчиков электрической энергии
    /// </summary>
    public interface IElectricEnergyMetersAPI
    {
        /// <summary>
        /// Метод для создания нового счётчика электрической энергии
        /// </summary>
        /// <param name="createElectricEnergyMeterDTO">
        /// DTO для нового счётчика электрической энергии
        /// </param>
        [HttpPost]
        Task<ElectricEnergyMeterDTO> CreateElectricEnergyMeter(CreateElectricEnergyMeterDTO createElectricEnergyMeterDTO);
        /// <summary>
        /// Метод для получения списка всех счётчиков электрической энергии
        /// </summary>
        [HttpGet("list")]
        Task<ElectricEnergyMetersListDTO> GetAll();
        /// <summary>
        /// Метод для проверки существования счётчика электрической энергии с указанным Id
        /// </summary>
        /// <param name="electricEnergyMeterId">
        /// Id счётчика электрической энергии
        /// </param>
        [HttpGet("exists")]
        Task<ElectricEnergyMeterExistenceDTO> CheckElectricEnergyMeterExists(int electricEnergyMeterId);
    }
}
