using System;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание трансформатора напряжения
    /// </summary>
    public class VoltageTransformer : TNEEntityBase
    {
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
        /// <summary>
        /// Представляет тип трансформатора
        /// </summary>
        public TransformerType TransformerType { get; set; }

        /// <summary>
        /// Точка измерения электроэнергии, к которой относится данный трансформатор напряжения
        /// </summary>
        public virtual ElectricityMeasuringPoint MeasuringPoint { get; set; }
    }
}
