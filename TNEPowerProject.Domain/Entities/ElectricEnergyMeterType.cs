using System.Collections.Generic;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание типа счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterType : TNEEntityBase
    {
        /// <summary>
        /// Описание (название) типа счётчика электрической энергии
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список счётчиков электрической энергии данного типа
        /// </summary>
        public virtual ICollection<ElectricEnergyMeter> ElectricEnergyMeters { get; set; }
    }
}
