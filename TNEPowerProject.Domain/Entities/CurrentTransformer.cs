using System;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание трансформатора тока
    /// </summary>
    public class CurrentTransformer
    {
        /// <summary>
        /// Уникальный идентификатор трансформатора тока
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер трансформатора тока
        /// </summary>
        public int Number { get; set; }
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
        /// Представляет тип трансформатора
        /// </summary>
        public TransformerType TransformerType { get; set; }

        /// <summary>
        /// Точка измерения электроэнергии, к которой относится данный трансформатор тока
        /// </summary>
        public virtual ElectricityMeasuringPoint MeasuringPoint { get; set; }
    }
}
