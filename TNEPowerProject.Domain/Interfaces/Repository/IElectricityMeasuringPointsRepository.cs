using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Domain.Interfaces.Repository
{
    /// <summary>
    /// Представляет интерфейс для репозитория точек измерения электроэнергии
    /// </summary>
    public interface IElectricityMeasuringPointsRepository : ITNERepository<ElectricityMeasuringPoint>
    {
    }
}
