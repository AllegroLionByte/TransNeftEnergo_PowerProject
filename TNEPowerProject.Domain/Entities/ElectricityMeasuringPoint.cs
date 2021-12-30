using System.Collections.Generic;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание точки измерения электроэнергии
    /// </summary>
    public class ElectricityMeasuringPoint : TNEEntityBase
    {
        /// <summary>
        /// Наименование точки измерения электроэнергии
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Уникальный идентификатор счётчика электрической энергии 
        /// </summary>
        public int ElectricEnergyMeterId { get; set; }
        /// <summary>
        /// Счётчик электрической энергии, относящийся к данной точки измерения электроэнергии
        /// </summary>
        public virtual ElectricEnergyMeter ElectricEnergyMeter { get; set; }
        /// <summary>
        /// Уникальный идентификатор трансформатора тока
        /// </summary>
        public int CurrentTransformerId { get; set; }
        /// <summary>
        /// Трансформатор тока, относящийся к данной точки измерения электроэнергии
        /// </summary>
        public virtual CurrentTransformer CurrentTransformer { get; set; }
        /// <summary>
        /// Уникальный идентификатор трансформатора напряжения
        /// </summary>
        public int VoltageTransformerId { get; set; }
        /// <summary>
        /// Трансформатор напряжения, относящийся к данной точки измерения электроэнергии
        /// </summary>
        public virtual VoltageTransformer VoltageTransformer { get; set; }

        /// <summary>
        /// Уникальный идентификатор объекта потребления, к которому относится данная точка измерения электроэнергии
        /// </summary>
        public int ElectricityConsumptionObjectId { get; set; }
        /// <summary>
        /// Объект потребления, к которому относится данная точка измерения электроэнергии
        /// </summary>
        public virtual ElectricityConsumptionObject ElectricityConsumptionObject { get; set; }

        /// <summary>
        /// Список связей по расчётному прибору учёта
        /// </summary>
        public virtual ICollection<AccountingDeviceInfo> AccountingDeviceInfos { get; set; }
    }
}
