using Newtonsoft.Json;
using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO
{
    /// <summary>
    /// Базовый класс для формирования ответа от REST API
    /// </summary>
    public class TNEBaseDTO<T> where T : ITNEDTO
    {
        /// <summary>
        /// Статус операции согласно спецификации REST API
        /// </summary>
        [JsonProperty]
        public int StatusCode { get; private set; }
        /// <summary>
        /// Сообщение о статусе операции
        /// </summary>
        [JsonProperty]
        public string Message { get; private set; }
        /// <summary>
        /// Результат выполнения операции
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// Конструирует ответ с указанием статуса операции 200 OK и пустым сообщением.
        /// </summary>
        /// <remarks>
        /// Используется при десериализации JSON
        /// </remarks>
        public TNEBaseDTO()
        {
            StatusCode = (int)RestResponseCode.OK;
            Message = "";
        }
        /// <summary>
        /// Конструирует ответ с указанием статуса операции
        /// </summary>
        /// <param name="statusCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public TNEBaseDTO(RestResponseCode statusCode) : this((int)statusCode, "") { }
        /// <summary>
        /// Конструирует ответ с указанием статуса операции и соответствующего сообщения
        /// </summary>
        /// <param name="statusCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public TNEBaseDTO(RestResponseCode statusCode, string message) : this((int)statusCode, message) { }
        /// <summary>
        /// [С осторожностью]
        /// Конструирует ответ с указанием специального статуса операции
        /// Внимание! Статусный код должен соответствовать принятым для REST API кодам ответов!
        /// </summary>
        /// <param name="statusCode">
        /// Статус операции
        /// </param>
        public TNEBaseDTO(int statusCode) : this(statusCode, "") { }
        /// <summary>
        /// [С осторожностью]
        /// Конструирует ответ с указанием специального статуса операции и соответствующего сообщения
        /// Внимание! Статусный код должен соответствовать принятым для REST API кодам ответов!
        /// </summary>
        /// <param name="statusCode">
        /// Статус операции
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public TNEBaseDTO(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = (string.IsNullOrWhiteSpace(message) ? "" : message);
        }
        /// <summary>
        /// Позволяет задать статус операции согласно спецификации REST API
        /// </summary>
        /// <param name="statusCode">
        /// Статус операции согласно спецификации REST API
        /// </param>
        public void SetStatus(RestResponseCode statusCode) => SetStatus((int)statusCode, "");
        /// <summary>
        /// Позволяет задать статус операции согласно спецификации REST API и соответствующее сообщение
        /// </summary>
        /// <param name="statusCode">
        /// Статус операции согласно спецификации REST API
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public void SetStatus(RestResponseCode statusCode, string message) => SetStatus((int)statusCode, message);
        /// <summary>
        /// Позволяет задать произвольный статус операции
        /// </summary>
        /// <param name="statusCode">
        /// Специальный статус операции
        /// </param>
        public void SetStatus(int statusCode) => SetStatus(statusCode, "");
        /// <summary>
        /// Позволяет задать специальный статус операции и соответствующее сообщение
        /// </summary>
        /// <param name="statusCode">
        /// Специальный статус операции
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public void SetStatus(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
