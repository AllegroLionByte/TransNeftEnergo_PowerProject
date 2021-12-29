using System.Collections.Generic;

namespace TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects
{
    /// <summary>
    /// Представляет DTO для списка объектов потребления
    /// </summary>
    public class ElectricityConsumptionObjectsListDTO : ITNEDTO
    {
        /// <summary>
        /// Представляет DTO для списка объектов потребления
        /// </summary>
        public ElectricityConsumptionObjectsListDTO()
        {
            ElectricityConsumptionObjects = new List<ElectricityConsumptionObjectListItemDTO>();
        }
        /// <summary>
        /// Список объектов потребления
        /// </summary>
        public IList<ElectricityConsumptionObjectListItemDTO> ElectricityConsumptionObjects { get; set; }
        /// <summary>
        /// DTO элемент списка для объектов потребления
        /// </summary>
        public class ElectricityConsumptionObjectListItemDTO
        {
            /// <summary>
            /// Уникальный идентификатор объекта потребления
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Наименование объекта потребления
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Id дочерней организации, ответственной за данный объект потребления
            /// </summary>
            public int SubOrganizationId { get; set; }
            /// <summary>
            /// Имя дочерней организации, ответственной за данный объект потребления
            /// </summary>
            public string SubOrganizationName { get; set; }
        }
    }
}
