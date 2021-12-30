using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;
using System.Linq.Expressions;
using System.Linq;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория точек измерения электроэнергии
    /// </summary>
    public class ElectricityMeasuringPointsRepository : TNERepository<ElectricityMeasuringPoint>, IElectricityMeasuringPointsRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория точек измерения электроэнергии
        /// </summary>
        public ElectricityMeasuringPointsRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
        /// <summary>
        /// Позволяет получить точку измерения электроэнергии по её Id (включая счётчик электрической энергии и трансформаторы тока и напряжения)
        /// </summary>
        public async override Task<ElectricityMeasuringPoint> GetById(int id)
        {
            try
            {
                return await dbContext.Set<ElectricityMeasuringPoint>()
                    .Include(x => x.ElectricEnergyMeter)
                    .Include(x => x.CurrentTransformer)
                    .Include(x => x.VoltageTransformer)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting element {Entity} by id: {Id}.", GetType(), typeof(ElectricityMeasuringPoint), id);
                return null;
            }
        }
        /// <summary>
        /// Позволяет получить список точек измерения электроэнергии по заданному условию, включив в результат трансформаторы и
        /// счётчики электрической энергии
        /// </summary>
        /// <param name="predicate">
        /// Выражение для конструкции Where
        /// </param>
        public async Task<IEnumerable<ElectricityMeasuringPoint>> FindWithIncludedEquipment(Expression<Func<ElectricityMeasuringPoint, bool>> predicate)
        {
            try
            {
                return await dbContext.Set<ElectricityMeasuringPoint>()
                    .Include(x => x.ElectricEnergyMeter)
                    .Include(x => x.CurrentTransformer)
                    .Include(x => x.VoltageTransformer)
                    .Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during finding elements {Entity} by {Pred}.", GetType(), typeof(ElectricityMeasuringPoint), predicate);
                return new List<ElectricityMeasuringPoint>();
            }
        }
    }
}
