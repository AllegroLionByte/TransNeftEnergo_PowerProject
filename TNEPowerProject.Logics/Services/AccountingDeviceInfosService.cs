using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO.AccountingDeviceInfos;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для расчётных приборов учёта
    /// </summary>
    public class AccountingDeviceInfosService : IAccountingDeviceInfosService
    {
        private readonly AccountingDeviceInfosRepository accountingDeviceInfosRepository;
        /// <summary>
        /// Представляет реализацию сервиса для расчётных приборов учёта
        /// </summary>
        public AccountingDeviceInfosService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для расчётных приборов учёта
        /// </summary>
        public AccountingDeviceInfosService(EnergoDBContext dbContext, ILogger logger)
        {
            accountingDeviceInfosRepository = new AccountingDeviceInfosRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет получить список всех расчётных приборов учёта для указанного года
        /// </summary>
        /// <param name="year">
        /// Год для поиска
        /// </param>
        public async Task<TNEBaseDTO<AccountingDeviceInfosListDTO>> GetAllAccountingDeviceInfosByYear(int year)
        {
            return new TNEBaseDTO<AccountingDeviceInfosListDTO>(RestResponseCode.OK)
            {
                Result = new AccountingDeviceInfosListDTO()
                {
                    AccountingDeviceInfos = (await accountingDeviceInfosRepository.Find(x => x.Interval_From.Year == year && x.Interval_To.Year == year))
                        .Select(x => new AccountingDeviceInfosListDTO.AccountingDeviceInfoListItemDTO()
                        {
                            Id = x.Id,
                            Interval_From = x.Interval_From,
                            Interval_To = x.Interval_To,
                            ConsumedElectricity = x.ConsumedElectricity,
                            ElectricityMeasuringPointId = x.ElectricityMeasuringPointId,
                            ElectricityMeasuringPointName = x.MeasuringPoint.Name,
                            ElectricitySupplyPointId = x.ElectricitySupplyPointId,
                            ElectricitySupplyPointName = x.SupplyPoint.Name
                        }).ToList()
                }
            };
        }
    }
}
