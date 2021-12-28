namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для проверки существования трансформатора тока
    /// </summary>
    public class CurrentTransformerExistenceDTO : ITNEDTO
    {
        /// <summary>
        /// Указывает, существует ли трансформатор тока с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
