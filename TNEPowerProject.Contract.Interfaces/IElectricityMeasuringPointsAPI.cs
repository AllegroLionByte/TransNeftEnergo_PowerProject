using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для точек измерения электроэнергии
    /// </summary>
    public interface IElectricityMeasuringPointsAPI
    {
        /// <summary>
        /// Метод для проверки существования точки измерения электроэнергии с указанным Id
        /// </summary>
        /// <param name="eMeasPointId">
        /// Id точки измерения электроэнергии
        /// </param>
        [HttpGet("{eMeasPointId}/exists")]
        [Get("/api/electricitymeasuringpoints/{eMeasPointId}/exists")]
        Task<TNEBaseDTO<ElectricityMeasuringPointExistenceDTO>> CheckElectricityMeasuringPointExists(int eMeasPointId);
        /// <summary>
        /// Метод для получения точки измерения электроэнергии по указанному Id
        /// </summary>
        /// <param name="eMeasPointId">
        /// Id точки измерения электроэнергии
        /// </param>
        [HttpGet("{eMeasPointId}")]
        [Get("/api/electricitymeasuringpoints/{eMeasPointId}")]
        Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> GetElectricityMeasuringPoint(int eMeasPointId);
        /// <summary>
        /// Метод для создания новой точки измерения электроэнергии
        /// </summary>
        /// <param name="createElectricityMeasuringPointDTO">
        /// DTO для новой точки измерения электроэнергии
        /// </param>
        [HttpPost]
        [Post("/api/electricitymeasuringpoints")]
        Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> CreateElectricityMeasuringPoint([Body] CreateElectricityMeasuringPointDTO createElectricityMeasuringPointDTO);
    }
}
