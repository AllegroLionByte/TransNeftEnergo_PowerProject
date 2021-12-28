namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для описания типа счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypeDTO : ITNEDTO
    {
        /// <summary>
        /// Уникальный идентификатор типа счётчика электрической энергии
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Описание (название) типа счётчика электрической энергии
        /// </summary>
        public string Description { get; set; }
    }
}
