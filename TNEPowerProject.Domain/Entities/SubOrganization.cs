using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание дочерней организации
    /// </summary>
    public class SubOrganization : TNEEntityBase
    {
        /// <summary>
        /// Имя организации
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес организации
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Id родительской организации
        /// </summary>
        public int ParentOrganizationId { get; set; }
        /// <summary>
        /// Родительская организация
        /// </summary>
        [ForeignKey("ParentOrganizationId")]
        public virtual Organization ParentOrganization { get; set; }
        /// <summary>
        /// Список объектов потребления для дочерней организации
        /// </summary>
        public virtual ICollection<ElectricityConsumptionObject> ElectricityConsumptionObjects { get; set; }
    }
}
