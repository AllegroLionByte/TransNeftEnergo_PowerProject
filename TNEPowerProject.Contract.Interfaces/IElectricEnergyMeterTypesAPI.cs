using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes;
using TNEPowerProject.Contract.DTO;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для типов счётчиков электрической энергии
    /// </summary>
    public interface IElectricEnergyMeterTypesAPI
    {
        /// <summary>
        /// Метод для создания нового типа счётчиков электрической энергии
        /// </summary>
        /// <param name="createElectricEnergyMeterTypeDTO">
        /// DTO для нового типа счётчика электрической энергии
        /// </param>
        [HttpPost]
        Task<TNEBaseDTO<ElectricEnergyMeterTypeDTO>> CreateElectricEnergyMeterType(CreateElectricEnergyMeterTypeDTO createElectricEnergyMeterTypeDTO);
        /// <summary>
        /// Метод для получения списка всех типов счётчиков электрической энергии
        /// </summary>
        [HttpGet("list")]
        [Get("/api/electricenergymetertypes/list")]
        Task<TNEBaseDTO<ElectricEnergyMeterTypesListDTO>> GetAll();
        /// <summary>
        /// Метод для проверки существования типа счётчика электрической энергии с указанным Id
        /// </summary>
        /// <param name="electricEnergyMeterTypeId">
        /// Id типа счётчика электрической энергии
        /// </param>
        [HttpGet("exists")]
        Task<TNEBaseDTO<ElectricEnergyMeterTypeExistenceDTO>> CheckElectricEnergyMeterTypeExists(int electricEnergyMeterTypeId);
    }
}
