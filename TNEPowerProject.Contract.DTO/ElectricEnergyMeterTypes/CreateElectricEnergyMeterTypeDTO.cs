namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для создания нового типа счётчика электрической энергии
    /// </summary>
    public class CreateElectricEnergyMeterTypeDTO
    {
        /// <summary>
        /// Описание (название) типа счётчика электрической энергии
        /// </summary>
        public string Description { get; set; }
    }
}
