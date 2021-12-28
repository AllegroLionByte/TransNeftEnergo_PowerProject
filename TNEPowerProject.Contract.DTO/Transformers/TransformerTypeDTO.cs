namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для описания типа трансформатора
    /// </summary>
    public class TransformerTypeDTO : ITNEDTO
    {
        /// <summary>
        /// Уникальный идентификатор типа трансформатора
        /// </summary>
        public int Id { get; set; }
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
