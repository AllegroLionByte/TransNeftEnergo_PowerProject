using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;

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

    }
}
