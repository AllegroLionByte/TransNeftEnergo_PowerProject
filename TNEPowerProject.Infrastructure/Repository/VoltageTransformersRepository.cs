using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;

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
        public VoltageTransformersRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger)
        {

        }

    }
}
