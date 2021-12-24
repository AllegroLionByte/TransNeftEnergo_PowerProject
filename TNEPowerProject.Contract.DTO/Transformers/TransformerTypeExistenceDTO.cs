using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для проверки существования типа трансформатора
    /// </summary>
    public class TransformerTypeExistenceDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для проверки существования типа трансформатора
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public TransformerTypeExistenceDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Представляет DTO для проверки существования типа трансформатора
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public TransformerTypeExistenceDTO(RestResponseCode responseCode, string message) : base(responseCode, message) { }
        /// <summary>
        /// Указывает, существует ли тип трансформатора с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
