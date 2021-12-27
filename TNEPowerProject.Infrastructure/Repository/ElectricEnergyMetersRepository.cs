using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMetersRepository : TNERepository<ElectricEnergyMeter>, IElectricEnergyMeterRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMetersRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
        /// <summary>
        /// Позволяет найти счётчики электрической энергии по заданному условию (в ответ включается Тип счётчика электрической энергии)
        /// </summary>
        public override async Task<IEnumerable<ElectricEnergyMeter>> Find(Expression<Func<ElectricEnergyMeter, bool>> predicate)
        {
            try
            {
                return await dbContext.Set<ElectricEnergyMeter>().Where(predicate).Include(x => x.EEMeterType).ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during finding elements {Entity} by {Pred}.", GetType(), typeof(ElectricEnergyMeter), predicate);
                return new List<ElectricEnergyMeter>();
            }
        }
        /// <summary>
        /// Позволяет получить список всех объектов
        /// </summary>
        public async override Task<IEnumerable<ElectricEnergyMeter>> GetAll()
        {
            try
            {
                return await dbContext.Set<ElectricEnergyMeter>().Include(x => x.EEMeterType).ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting all elements {Entity}.", GetType(), typeof(ElectricEnergyMeter));
                return new List<ElectricEnergyMeter>();
            }
        }
        /// <summary>
        /// Позволяет получить счётчик электрической энергии по его Id
        /// </summary>
        public async override Task<ElectricEnergyMeter> GetById(int id)
        {
            try
            {
                return await dbContext.Set<ElectricEnergyMeter>().Include(x => x.EEMeterType).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting element {Entity} by id: {Id}.", GetType(), typeof(ElectricEnergyMeter), id);
                return null;
            }
        }
    }
}
