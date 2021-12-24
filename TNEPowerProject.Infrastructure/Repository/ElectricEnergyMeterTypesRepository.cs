using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория типов счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypesRepository : TNERepository<ElectricEnergyMeterType>, IElectricEnergyMeterTypesRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория типов счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMeterTypesRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
    }
}
