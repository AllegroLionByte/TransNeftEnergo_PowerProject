using System.Collections.Generic;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание типа счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterType
    {
        /// <summary>
        /// Уникальный идентификатор типа счётчика электрической энергии
        /// </summary>
        public int Id { get; set; }
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
