using System;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание расчётного прибора учёта
    /// </summary>
    public class AccountingDeviceInfo : TNEEntityBase
    {
        /// <summary>
        /// Указывает начало расчётного периода
        /// </summary>
        public DateTime Interval_From { get; set; }
        /// <summary>
        /// Указывает конец расчётного периода
        /// </summary>
        public DateTime Interval_To { get; set; }
        /// <summary>
        /// Указывает количество потреблённой электроэнергии за расчётный период
        /// </summary>
        public double ConsumedElectricity { get; set; }
        /// <summary>
        /// Указывает на Id использовавшейся точки поставки электроэнергии
        /// </summary>
        public int ElectricitySupplyPointId { get; set; }
        /// <summary>
        /// Указывает на использовавшуюся точку поставки электроэнергии
        /// </summary>
        public virtual ElectricitySupplyPoint SupplyPoint { get; set; }
        /// <summary>
        /// Указывает на Id использовавшейся точки измерения электроэнергии
        /// </summary>
        public int ElectricityMeasuringPointId { get; set; }
        /// <summary>
        /// Указывает на использовавшуюся точку измерения электроэнергии
        /// </summary>
        public virtual ElectricityMeasuringPoint MeasuringPoint { get; set; }
    }
}
