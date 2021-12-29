using System;
using System.Collections.Generic;

namespace TNEPowerProject.Contract.DTO.AccountingDeviceInfos
{
    /// <summary>
    /// Представляет DTO для списка расчётных приборов учёта
    /// </summary>
    public class AccountingDeviceInfosListDTO : ITNEDTO
    {
        /// <summary>
        /// Представляет DTO для списка расчётных приборов учёта
        /// </summary>
        public AccountingDeviceInfosListDTO()
        {
            AccountingDeviceInfos = new List<AccountingDeviceInfoListItemDTO>();
        }
        /// <summary>
        /// Список расчётных приборов учёта
        /// </summary>
        public IList<AccountingDeviceInfoListItemDTO> AccountingDeviceInfos { get; set; }
        /// <summary>
        /// DTO элемент списка для расчётных приборов учёта
        /// </summary>
        public class AccountingDeviceInfoListItemDTO
        {
            /// <summary>
            /// Уникальный идентификатор расчётного прибора учёта
            /// </summary>
            public int Id { get; set; }
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
            /// Указывает на имя использовавшейся точки поставки электроэнергии
            /// </summary>
            public string ElectricitySupplyPointName { get; set; }
            /// <summary>
            /// Указывает на Id использовавшейся точки измерения электроэнергии
            /// </summary>
            public int ElectricityMeasuringPointId { get; set; }
            /// <summary>
            /// Указывает на имя использовавшейся точки измерения электроэнергии
            /// </summary>
            public string ElectricityMeasuringPointName { get; set; }
        }
    }
}
