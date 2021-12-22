using System.Collections.Generic;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет точку поставки электроэнергии
    /// </summary>
    public class ElectricitySupplyPoint : TNEEntityBase
    {
        /// <summary>
        /// Наименование точки поставки электроэнергии
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Максимальная мощность, выраженная в кВт
        /// </summary>
        public double MaximumPower { get; set; }

        /// <summary>
        /// Уникальный идентификатор объекта потребления, к которому относится данная точка поставки электроэнергии
        /// </summary>
        public int ElectricityConsumptionObjectId { get; set; }
        /// <summary>
        /// Объект потребления, к которому относится данная точка поставки электроэнергии
        /// </summary>
        public ElectricityConsumptionObject ConsumptionObject { get; set; }

        /// <summary>
        /// Список связей по расчётному прибору учёта
        /// </summary>
        public virtual ICollection<AccountingDeviceInfo> AccountingDeviceInfos { get; set; }
    }
}
