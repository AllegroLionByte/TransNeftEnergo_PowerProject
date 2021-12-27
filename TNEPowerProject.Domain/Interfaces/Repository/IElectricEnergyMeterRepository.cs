using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Domain.Interfaces.Repository
{
    /// <summary>
    /// Представляет интерфейс для репозитория счётчиков электрической энергии
    /// </summary>
    public interface IElectricEnergyMeterRepository : ITNERepository<ElectricEnergyMeter>
    {
    }
}
