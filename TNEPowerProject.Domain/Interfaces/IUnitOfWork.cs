using System.Threading.Tasks;
using TNEPowerProject.Domain.Abstract;
using TNEPowerProject.Domain.Interfaces.Repository;

namespace TNEPowerProject.Domain.Interfaces
{
    /// <summary>
    /// Представляет интерфейс для паттерна Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Позволяет получить объект репозитория
        /// </summary>
        ITNERepository<T> AsyncRepository<T>() where T : TNEEntityBase;
        /// <summary>
        /// Позволяет применить изменения к БД
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
