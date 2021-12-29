using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для точек измерения электроэнергии
    /// </summary>
    public interface IElectricityMeasuringPointsService
    {
        /// <summary>
        /// Позволяет добавить новую точку измерения электроэнергии
        /// </summary>
        /// <param name="createElectricityMeasuringPointDTO">
        /// DTO для новой точки измерения электроэнергии
        /// </param>
        Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> CreateElectricityMeasuringPoint(CreateElectricityMeasuringPointDTO createElectricityMeasuringPointDTO);
        /// <summary>
        /// Позволяет проверить существование точки измерения электроэнергии с указанным Id
        /// </summary>
        /// <param name="eMeasPointId">
        /// Id точки измерения электроэнергии
        /// </param>
        Task<TNEBaseDTO<ElectricityMeasuringPointExistenceDTO>> CheckElectricityMeasuringPointExists(int eMeasPointId);
        /// <summary>
        /// Позволяет получить точку измерения электроэнергии по указанному Id
        /// </summary>
        /// <param name="eMeasPointId">
        /// Id точки измерения электроэнергии
        /// </param>
        Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> GetElectricityMeasuringPoint(int eMeasPointId);
    }
}
