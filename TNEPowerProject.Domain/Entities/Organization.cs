using System.Collections.Generic;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание организации
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Уникальный идентификатор организации
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
        /// Список дочерних организаций
        /// </summary>
        public virtual ICollection<SubOrganization> SubOrganizations { get; set; }
    }
}
