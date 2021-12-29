using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория объектов потребления
    /// </summary>
    public class ElectricityConsumptionObjectsRepository : TNERepository<ElectricityConsumptionObject>, IElectricityConsumptionObjectsRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория объектов потребления
        /// </summary>
        public ElectricityConsumptionObjectsRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
        /// <summary>
        /// Позволяет получить список всех объектов потребления
        /// </summary>
        public override async Task<IEnumerable<ElectricityConsumptionObject>> GetAll()
        {
            try
            {
                return await dbContext.Set<ElectricityConsumptionObject>()
                    .Include(x => x.SubOrganization)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting all elements {Entity}.", GetType(), typeof(ElectricityConsumptionObject));
                return new List<ElectricityConsumptionObject>();
            }
        }
    }
}
