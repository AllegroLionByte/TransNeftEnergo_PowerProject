using System;
using TNEPowerProject.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeter : TNEEntityBase
    {
        /// <summary>
        /// Номер счётчика электрической энергии
        /// </summary>
        public long Number { get; set; }
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
        public virtual ElectricEnergyMeterType EEMeterType { get; set; }

        /// <summary>
        /// Точка измерения электроэнергии, к которой относится данный счётчик электрической энергии
        /// </summary>
        public virtual ElectricityMeasuringPoint MeasuringPoint { get; set; }
    }
}
