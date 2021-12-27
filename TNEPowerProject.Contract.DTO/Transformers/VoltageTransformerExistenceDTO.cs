using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для проверки существования трансформатора напряжения
    /// </summary>
    public class VoltageTransformerExistenceDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для проверки существования трансформатора напряжения
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public VoltageTransformerExistenceDTO(RestResponseCode responseCode) : base(responseCode) { }
        /// <summary>
        /// Указывает, существует ли трансформатор напряжения с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
