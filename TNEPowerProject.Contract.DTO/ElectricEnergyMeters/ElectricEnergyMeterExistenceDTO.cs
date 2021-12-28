namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeters
{
    /// <summary>
    /// Представляет DTO для проверки существования счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterExistenceDTO : ITNEDTO
    {
        /// <summary>
        /// Указывает, существует ли счётчик электрической энергии с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
