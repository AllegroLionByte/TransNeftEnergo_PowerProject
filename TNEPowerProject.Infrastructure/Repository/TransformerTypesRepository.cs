using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория типов трансформаторов
    /// </summary>
    public class TransformerTypesRepository : TNERepository<TransformerType>, ITransformerTypesRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория типов трансформаторов
        /// </summary>
        public TransformerTypesRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
    }
}
