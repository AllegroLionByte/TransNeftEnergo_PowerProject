using System;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeters
{
    /// <summary>
    /// Представляет DTO для описания счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterDTO : ITNEDTO
    {
        /// <summary>
        /// Уникальный идентификатор счётчика электрической энергии
        /// </summary>
        public int Id { get; set; }
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
        /// Описание (название) типа счётчика электрической энергии
        /// </summary>
        public string EEMeterTypeDescription { get; set; }
    }
}
