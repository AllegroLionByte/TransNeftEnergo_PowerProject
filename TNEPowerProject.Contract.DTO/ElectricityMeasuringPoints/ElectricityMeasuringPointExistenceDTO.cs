namespace TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints
{
    /// <summary>
    /// Представляет DTO для проверки существования точки измерения электроэнергии
    /// </summary>
    public class ElectricityMeasuringPointExistenceDTO : ITNEDTO
    {
        /// <summary>
        /// Указывает, существует ли точка измерения электроэнергии с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
