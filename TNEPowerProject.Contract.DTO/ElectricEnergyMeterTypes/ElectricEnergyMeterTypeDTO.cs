using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes
{
    /// <summary>
    /// Представляет DTO для описания типа счётчика электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypeDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для описания типа счётчика электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public ElectricEnergyMeterTypeDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Представляет DTO для описания типа счётчика электрической энергии
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public ElectricEnergyMeterTypeDTO(RestResponseCode responseCode, string message) : base(responseCode, message) { }
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
