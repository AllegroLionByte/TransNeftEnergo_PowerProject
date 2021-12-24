using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для проверки существования типа счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypeExistenceDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для проверки существования типа счётчика электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public ElectricEnergyMeterTypeExistenceDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Представляет DTO для проверки существования типа счётчика электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public ElectricEnergyMeterTypeExistenceDTO(RestResponseCode responseCode, string message) : base(responseCode, message) { }
        /// <summary>
        /// Указывает, существует ли тип счётчика электрической энергии с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
