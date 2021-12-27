using System;
using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для описания создаваемого трансформатора напряжения
    /// </summary>
    public class CreateVoltageTransformerDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для описания создаваемого трансформатора напряжения
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public CreateVoltageTransformerDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Номер трансформатора напряжения
        /// </summary>
        public long Number { get; set; }
        /// <summary>
        /// КТТ - коэфициент трансформации по напряжению
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
    }
}
