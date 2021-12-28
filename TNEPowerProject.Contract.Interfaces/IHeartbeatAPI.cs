using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет API для проверки жизнеспособности API
    /// </summary>
    public interface IHeartbeatAPI
    {
        /// <summary>
        /// Метод для проверки жизнеспособности API. Возвращает строку вида I am alive! Your string: [переданная_строка]
        /// </summary>
        /// <param name="checkingString">Строка, которая будет возвращена в ответ</param>
        [HttpGet("{checkingString}")]
        [Get("/api/Heartbeat/{checkingString}")]
        Task<string> Heartbeat(string checkingString);
    }
}
