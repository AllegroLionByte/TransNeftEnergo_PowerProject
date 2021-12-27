using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для проверки существования трансформатора тока
    /// </summary>
    public class CurrentTransformerExistenceDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для проверки существования трансформатора тока
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public CurrentTransformerExistenceDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Указывает, существует ли трансформатор тока с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
