using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Domain.Interfaces.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет реализацию репозитория расчётных приборов учёта
    /// </summary>
    public class AccountingDeviceInfosRepository : TNERepository<AccountingDeviceInfo>, IAccountingDeviceInfosRepository
    {
        /// <summary>
        /// Представляет реализацию репозитория расчётных приборов учёта
        /// </summary>
        public AccountingDeviceInfosRepository(DbContext dbContext, ILogger logger) : base(dbContext, logger) { }
        /// <summary>
        /// Позволяет найти расчётные приборы учёта по заданному условию
        /// </summary>
        public override async Task<IEnumerable<AccountingDeviceInfo>> Find(Expression<Func<AccountingDeviceInfo, bool>> predicate)
        {
            try
            {
                return await dbContext.Set<AccountingDeviceInfo>()
                    .Include(x => x.MeasuringPoint)
                    .Include(x => x.SupplyPoint)
                    .Where(predicate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during finding elements {Entity} by {Pred}.", GetType(), typeof(AccountingDeviceInfo), predicate);
                return new List<AccountingDeviceInfo>();
            }
        }
    }
}
