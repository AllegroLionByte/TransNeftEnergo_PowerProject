using System.Collections.Generic;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание объекта потребления
    /// </summary>
    public class ElectricityConsumptionObject
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
        public IList<ElectricityMeasuringPoint> MeasuringPoints { get; set; }
        /// <summary>
        /// Список точек поставки электроэнергии, входящих в объект потребления
        /// </summary>
        public IList<ElectricitySupplyPoint> SupplyPoints { get; set; }
    }
}
