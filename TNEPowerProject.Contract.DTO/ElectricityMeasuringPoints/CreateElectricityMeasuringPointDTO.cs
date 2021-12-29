namespace TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints
{
    /// <summary>
    /// Представляет DTO для создания новой точки измерения электроэнергии
    /// </summary>
    public class CreateElectricityMeasuringPointDTO
    {
        /// <summary>
        /// Наименование точки измерения электроэнергии
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Уникальный идентификатор счётчика электрической энергии, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public int ElectricEnergyMeterId { get; set; }
        /// <summary>
        /// Уникальный идентификатор трансформатора тока, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public int CurrentTransformerId { get; set; }
        /// <summary>
        /// Уникальный идентификатор трансформатора напряжения, относящегося к данной точке измерения электроэнергии
        /// </summary>
        public int VoltageTransformerId { get; set; }
        /// <summary>
        /// Уникальный идентификатор объекта потребления, к которому прикрепляется данная точка измерения электроэнергии
        /// </summary>
        public int ElectricityConsumptionObjectId { get; set; }
    }
}
