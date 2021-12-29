using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.AccountingDeviceInfos;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для расчётных приборов учёта
    /// </summary>
    public interface IAccountingDeviceInfosService
    {
        /// <summary>
        /// Позволяет получить список всех расчётных приборов учёта для указанного года
        /// </summary>
        /// <param name="year">
        /// Год для поиска
        /// </param>
        Task<TNEBaseDTO<AccountingDeviceInfosListDTO>> GetAllAccountingDeviceInfosByYear(int year);
    }
}
