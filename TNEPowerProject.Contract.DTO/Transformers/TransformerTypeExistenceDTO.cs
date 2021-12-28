namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для проверки существования типа трансформатора
    /// </summary>
    public class TransformerTypeExistenceDTO : ITNEDTO
    {
        /// <summary>
        /// Указывает, существует ли тип трансформатора с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
