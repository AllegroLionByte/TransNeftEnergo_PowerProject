using System.Collections.Generic;
using TNEPowerProject.Domain.Interfaces;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание организации
    /// </summary>
    public class Organization : TNEEntityBase
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
        /// Список дочерних организаций
        /// </summary>
        public virtual ICollection<SubOrganization> SubOrganizations { get; set; }
    }
}
