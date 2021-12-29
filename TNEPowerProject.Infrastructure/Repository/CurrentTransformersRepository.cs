using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория трансформаторов тока
    /// </summary>
    public class CurrentTransformersRepository : TNERepository<CurrentTransformer>, ICurrentTransformersRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория трансформаторов тока
        /// </summary>
        public CurrentTransformersRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
        /// <summary>
        /// Позволяет получить трансформатор тока по его Id
        /// </summary>
        public override async Task<CurrentTransformer> GetById(int id)
        {
            try
            {
                return await dbContext.Set<CurrentTransformer>()
                    .Include(x => x.MeasuringPoint)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting element {Entity} by id: {Id}.", GetType(), typeof(CurrentTransformer), id);
                return default;
            }
        }
    }
}
