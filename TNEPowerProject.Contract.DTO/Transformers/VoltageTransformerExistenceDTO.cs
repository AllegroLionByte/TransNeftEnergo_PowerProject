namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для проверки существования трансформатора напряжения
    /// </summary>
    public class VoltageTransformerExistenceDTO : ITNEDTO
    {
        /// <summary>
        /// Указывает, существует ли трансформатор напряжения с указанным Id
        /// </summary>
        public bool Exists { get; set; }
    }
}
