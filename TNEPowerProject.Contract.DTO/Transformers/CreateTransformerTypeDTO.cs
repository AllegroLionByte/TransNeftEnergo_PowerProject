using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для создания нового типа трансформатора
    /// </summary>
    public class CreateTransformerTypeDTO
    {
        /// <summary>
        /// Род работы трансформатора
        /// </summary>
        public int TransformerPurpose { get; set; }
        /// <summary>
        /// Описание (название) типа трансформатора
        /// </summary>
        public string Description { get; set; }
    }
}
