using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Abstract;
using TNEPowerProject.Domain.Interfaces;
using TNEPowerProject.Domain.Interfaces.Repository;
using TNEPowerProject.Infrastructure.Database.EFCore;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию для паттерна Unit of Work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnergoDBContext dbContext;
        private readonly ILogger logger;
        /// <summary>
        /// Представляет реализацию для паттерна Unit of Work
        /// </summary>
        public UnitOfWork(EnergoDBContext dbContext, ILogger logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        /// <summary>
        /// Позволяет получить объект репозитория
        /// </summary>
        public ITNERepository<T> AsyncRepository<T>() where T : TNEEntityBase
        {
            return new TNERepository<T>(dbContext, logger);
        }
        /// <summary>
        /// Позволяет применить изменения к БД
        /// </summary>
        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
