using System.Collections.Generic;

namespace TNEPowerProject.Contract.DTO.ExpiredElectricEquipment
{
    /// <summary>
    /// Представляет DTO для списка объектов электрической инфраструктуры (трансформаторов и счётчиков электрической энергии)
    /// с истёкшими сроками поверки
    /// </summary>
    public class ExpiredElectricEquipmentListDTO<T> : ITNEDTO where T : ITNEDTO
    {
        /// <summary>
        /// Уникальный идентификатор объекта потребления, для которого выполняется поиск
        /// </summary>
        public int ConsumptionObjectId { get; set; }
        /// <summary>
        /// Наименование объекта потребления, для которого выполняется поиск
        /// </summary>
        public string ConsumptionObjectName { get; set; }
        /// <summary>
        /// Список объектов электрической инфраструктуры (трансформаторов и счётчиков электрической энергии)
        /// с истёкшими сроками поверки
        /// </summary>
        public IList<T> ExpiredElectricEquipment { get; set; }
        /// <summary>
        /// Представляет DTO для списка объектов электрической инфраструктуры (трансформаторов и счётчиков электрической энергии)
        /// с истёкшими сроками поверки
        /// </summary>
        public ExpiredElectricEquipmentListDTO()
        {
            ExpiredElectricEquipment = new List<T>();
        }
    }
}
