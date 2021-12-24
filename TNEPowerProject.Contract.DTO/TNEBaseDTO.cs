using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO
{
    /// <summary>
    /// Базовый класс для формирования ответа от REST API
    /// </summary>
    public abstract class TNERestfulBaseDTO
    {
        /// <summary>
        /// Статус операции согласно спецификации REST API
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Конструирует ответ с указанием статуса операции
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public TNERestfulBaseDTO(RestResponseCode responseCode) : this((int)responseCode) { }
        /// <summary>
        /// [С осторожностью]
        /// Конструирует ответ с указанием специального статуса операции.
        /// Внимание! Статусный код должен соответствовать принятым для REST API кодам ответов!
        /// </summary>
        /// <param name="respCode">Статус операции</param>
        public TNERestfulBaseDTO(int respCode)
        {
            StatusCode = respCode;
        }
    }
}
