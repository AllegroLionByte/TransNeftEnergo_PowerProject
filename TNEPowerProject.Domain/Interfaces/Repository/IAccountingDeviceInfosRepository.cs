using System.Threading.Tasks;
using System.Collections.Generic;
using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Domain.Interfaces.Repository
{
    /// <summary>
    /// Представляет интерфейс для репозитория расчётных приборов учёта
    /// </summary>
    public interface IAccountingDeviceInfosRepository : ITNERepository<AccountingDeviceInfo>
    {
    }
}
