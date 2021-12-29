using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория трансформаторов напряжения
    /// </summary>
    public class VoltageTransformersRepository : TNERepository<VoltageTransformer>, IVoltageTransformersRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория трансформаторов напряжения
        /// </summary>
        public VoltageTransformersRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
        /// <summary>
        /// Позволяет получить трансформатор напряжения по его Id
        /// </summary>
        public override async Task<VoltageTransformer> GetById(int id)
        {
            try
            {
                return await dbContext.Set<VoltageTransformer>()
                    .Include(x => x.MeasuringPoint)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting element {Entity} by id: {Id}.", GetType(), typeof(VoltageTransformer), id);
                return default;
            }
        }
    }
}
