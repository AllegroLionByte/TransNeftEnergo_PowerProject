using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO.AccountingDeviceInfos;
using TNEPowerProject.Contract.DTO;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для расчётного прибора учёта
    /// </summary>
    public interface IAccountingDeviceInfosAPI
    {
        /// <summary>
        /// Метод для получения списка расчетных приборов в выбранном году
        /// </summary>
        /// <param name="year">
        /// Год для поиска
        /// </param>
        [HttpGet("byyear")]
        [Get("/api/accountingdeviceinfos/byyear?year={year}")]
        Task<TNEBaseDTO<AccountingDeviceInfosListDTO>> GetAllByYear(int year);
    }
}
