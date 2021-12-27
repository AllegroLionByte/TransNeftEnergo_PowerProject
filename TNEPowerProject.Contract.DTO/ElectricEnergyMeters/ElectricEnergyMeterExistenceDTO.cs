using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeters
{
    /// <summary>
    /// Представляет DTO для проверки существования счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterExistenceDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для проверки существования счётчика электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public ElectricEnergyMeterExistenceDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Представляет DTO для проверки существования счётчика электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public ElectricEnergyMeterExistenceDTO(RestResponseCode responseCode, string message) : base(responseCode, message) { }
        /// <summary>
        /// Указывает, существует ли счётчик электрической энергии с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
