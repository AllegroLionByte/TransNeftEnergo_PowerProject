using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание дочерней организации
    /// </summary>
    public class SubOrganization
    {
        /// <summary>
        /// Уникальный идентификатор дочерней организации
        /// </summary>
        public int Id { get; set; }
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
        public Organization ParentOrganization { get; set; }
        /// <summary>
        /// Список объектов потребления для дочерней организации
        /// </summary>
        public IList<ElectricityConsumptionObject> ElectricityConsumptionObjects { get; set; }
    }
}
