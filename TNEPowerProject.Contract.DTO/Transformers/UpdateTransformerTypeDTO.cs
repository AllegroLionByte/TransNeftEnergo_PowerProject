namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для обновления типа трансформатора
    /// </summary>
    public class UpdateTransformerTypeDTO
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
