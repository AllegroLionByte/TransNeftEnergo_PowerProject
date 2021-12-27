using System;
using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для описания трансформатора тока
    /// </summary>
    public class CurrentTransformerDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для описания трансформатора тока
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public CurrentTransformerDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Представляет DTO для описания трансформатора тока
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public CurrentTransformerDTO(RestResponseCode responseCode, string message) : base(responseCode, message) { }
        /// <summary>
        /// Уникальный идентификатор трансформатора тока
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер трансформатора тока
        /// </summary>
        public long Number { get; set; }
        /// <summary>
        /// КТТ - коэфициент трансформации по току
        /// </summary>
        public double TransformationRatio { get; set; }
        /// <summary>
        /// Дата последней поверки
        /// </summary>
        public DateTime VerificationDate { get; set; }
        /// <summary>
        /// Срок поверки (указывает на дату окончания поверочного периода)
        /// </summary>
        public DateTime VerificationPeriod { get; set; }
        /// <summary>
        /// Уникальный идентификатор типа трансформатора
        /// </summary>
        public int TransformerTypeId { get; set; }
        /// <summary>
        /// Описание (название) типа трансформатора
        /// </summary>
        public string TransformerTypeDescription { get; set; }
    }
}
