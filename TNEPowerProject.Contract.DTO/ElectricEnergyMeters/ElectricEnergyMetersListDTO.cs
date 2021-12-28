using System;
using System.Collections.Generic;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeters
{
    /// <summary>
    /// Представляет DTO для списка счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMetersListDTO : ITNEDTO
    {
        /// <summary>
        /// Представляет DTO для списка счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMetersListDTO()
        {
            ElectricEnergyMeters = new List<ElectricEnergyMeterListItemDTO>();
        }
        /// <summary>
        /// Список счётчиков электрической энергии
        /// </summary>
        public IList<ElectricEnergyMeterListItemDTO> ElectricEnergyMeters { get; set; }
        /// <summary>
        /// DTO элемент списка для счётчиков электрической энергии
        /// </summary>
        public class ElectricEnergyMeterListItemDTO
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
}
