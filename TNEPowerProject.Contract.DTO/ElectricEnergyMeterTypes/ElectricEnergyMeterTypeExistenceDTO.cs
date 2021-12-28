namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для проверки существования типа счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypeExistenceDTO : ITNEDTO
    {
        /// <summary>
        /// Указывает, существует ли тип счётчика электрической энергии с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
