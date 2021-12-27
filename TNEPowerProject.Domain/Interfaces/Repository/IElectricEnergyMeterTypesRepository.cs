using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Domain.Interfaces.Repository
{
    /// <summary>
    /// Представляет интерфейс для репозитория типов счётчиков электрической энергии
    /// </summary>
    public interface IElectricEnergyMeterTypesRepository : ITNERepository<ElectricEnergyMeterType>
    {
    }
}
