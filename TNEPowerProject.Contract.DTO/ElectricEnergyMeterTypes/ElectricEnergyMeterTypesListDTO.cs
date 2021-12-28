using System.Collections.Generic;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для списка типов счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypesListDTO : ITNEDTO
    {
        /// <summary>
        /// Представляет DTO для списка типов счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMeterTypesListDTO()
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
