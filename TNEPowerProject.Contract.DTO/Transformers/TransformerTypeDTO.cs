using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для описания типа трансформатора
    /// </summary>
    public class TransformerTypeDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для описания типа трансформатора
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public TransformerTypeDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Род работы трансформатора
        /// </summary>
        public TransformerPurpose TransformerPurpose { get; set; }
        /// <summary>
        /// Уникальный идентификатор типа трансформатора
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Описание (название) типа трансформатора
        /// </summary>
        public string Description { get; set; }
    }
}
