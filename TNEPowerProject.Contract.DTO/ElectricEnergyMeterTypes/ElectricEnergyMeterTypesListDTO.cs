using System.Collections.Generic;
using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для списка типов счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypesListDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для списка типов счётчиков электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public ElectricEnergyMeterTypesListDTO(RestResponseCode responseCode) : this(responseCode, "") { }
        /// <summary>
        /// Представляет DTO для списка типов счётчиков электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public ElectricEnergyMeterTypesListDTO(RestResponseCode responseCode, string message) : base(responseCode, message)
        {
            ElectricEnergyMeterTypes = new List<ElectricEnergyMeterTypeListItemDTO>();
        }
        /// <summary>
        /// Список типов счётчиков электрической энергии
        /// </summary>
        public ICollection<ElectricEnergyMeterTypeListItemDTO> ElectricEnergyMeterTypes { get; set; }
        /// <summary>
        /// DTO элемент списка для типов счётчиков электрической энергии
        /// </summary>
        public class ElectricEnergyMeterTypeListItemDTO
        {
            /// <summary>
            /// Уникальный идентификатор типа счётчика электрической энергии
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Описание (название) типа счётчика электрической энергии
            /// </summary>
            public string Description { get; set; }
        }
    }
}
