using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.Interfaces;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.AccountingDeviceInfos;

namespace TNEPowerProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingDeviceInfosController : ControllerBase, IAccountingDeviceInfosAPI
    {
        private readonly IAccountingDeviceInfosService accountingDeviceInfosService;
        public AccountingDeviceInfosController(IAccountingDeviceInfosService accountingDeviceInfosService)
        {
            this.accountingDeviceInfosService = accountingDeviceInfosService;
        }

        [HttpGet("byyear")]
        public async Task<TNEBaseDTO<AccountingDeviceInfosListDTO>> GetAllByYear(int year)
        {
            return await accountingDeviceInfosService.GetAllAccountingDeviceInfosByYear(year);
        }
    }
}
