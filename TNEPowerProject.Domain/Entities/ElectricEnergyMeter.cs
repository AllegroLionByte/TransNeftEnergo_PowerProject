using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeter
    {
        /// <summary>
        /// Уникальный идентификатор счётчика электрической энергии
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер счётчика электрической энергии
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Дата последней поверки
        /// </summary>
        public DateTime VerificationDate { get; set; }
        /// <summary>
        /// Срок поверки (указывает на дату окончания поверочного периода)
        /// </summary>
        public DateTime VerificationPeriod { get; set; }
        /// <summary>
        /// Уникальный идентификатор типа счётчика электрической энергии
        /// </summary>
        public int EEMeterTypeId { get; set; }
        /// <summary>
        /// Представляет тип счётчика электрической энергии
        /// </summary>
        [ForeignKey("EEMeterTypeId")]
        public ElectricEnergyMeterType EEMeterType { get; set; }

        /// <summary>
        /// Точка измерения электроэнергии, к которой относится данный счётчик электрической энергии
        /// </summary>
        public virtual ElectricityMeasuringPoint MeasuringPoint { get; set; }
    }
}
