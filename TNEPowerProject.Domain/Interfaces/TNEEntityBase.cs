
namespace TNEPowerProject.Domain.Interfaces
{
    /// <summary>
    /// Представляет общую базовую сущность для всех сущностей проекта TNEPowerProject
    /// </summary>
    public abstract class TNEEntityBase
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        public int Id { get; set; }
    }
}
