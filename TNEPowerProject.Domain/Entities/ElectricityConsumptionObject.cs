using System.Collections.Generic;
using TNEPowerProject.Domain.Interfaces;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание объекта потребления
    /// </summary>
    public class ElectricityConsumptionObject : TNEEntityBase
    {
        /// <summary>
        /// Наименование объекта потребления
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес объекта потребления
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Id дочерней организации, ответственной за данный объект потребления
        /// </summary>
        public int SubOrganizationId { get; set; }
        /// <summary>
        /// Дочерняя организация, ответственная за данный объект потребления
        /// </summary>
        public SubOrganization SubOrganization { get; set; }
        /// <summary>
        /// Список точек измерения электроэнергии, входящих в объект потребления
        /// </summary>
        public virtual ICollection<ElectricityMeasuringPoint> MeasuringPoints { get; set; }
        /// <summary>
        /// Список точек поставки электроэнергии, входящих в объект потребления
        /// </summary>
        public virtual ICollection<ElectricitySupplyPoint> SupplyPoints { get; set; }
    }
}
