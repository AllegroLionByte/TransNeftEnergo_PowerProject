using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects;
using TNEPowerProject.Contract.DTO;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для объектов потребления
    /// </summary>
    public interface IElectricityConsumptionObjectsAPI
    {
        /// <summary>
        /// Метод для получения списка всех объектов потребления
        /// </summary>
        [HttpGet]
        [Get("/api/electricityconsumptionobjects")]
        Task<TNEBaseDTO<ElectricityConsumptionObjectsListDTO>> GetAll();
    }
}
