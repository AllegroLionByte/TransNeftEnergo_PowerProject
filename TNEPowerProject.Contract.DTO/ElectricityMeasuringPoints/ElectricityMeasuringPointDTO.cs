namespace TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints
{
    /// <summary>
    /// Представляет DTO для точки измерения электроэнергии
    /// </summary>
    public class ElectricityMeasuringPointDTO : ITNEDTO
    {
        /// <summary>
        /// Уникальный идентификатор точки измерения электроэнергии
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование точки измерения электроэнергии
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Уникальный идентификатор счётчика электрической энергии, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public int ElectricEnergyMeterId { get; set; }
        /// <summary>
        /// Номер счётчика электрической энергии, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public long ElectricEnergyMeterNumber { get; set; }
        /// <summary>
        /// Уникальный идентификатор трансформатора тока, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public int CurrentTransformerId { get; set; }
        /// <summary>
        /// Номер трансформатора тока, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public long CurrentTransformerNumber { get; set; }
        /// <summary>
        /// Уникальный идентификатор трансформатора напряжения, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public int VoltageTransformerId { get; set; }
        /// <summary>
        /// Номер трансформатора напряжения, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public long VoltageTransformerNumber { get; set; }
        /// <summary>
        /// Уникальный идентификатор объекта потребления, к которому прикреплена данная точка измерения электроэнергии
        /// </summary>
        public int ElectricityConsumptionObjectId { get; set; }
    }
}
