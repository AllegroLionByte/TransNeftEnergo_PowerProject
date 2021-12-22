using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using TNEPowerProject.Domain.Abstract;

namespace TNEPowerProject.Domain.Interfaces.Repository
{
    /// <summary>
    /// Представляет интерфейс репозитория в проекте TNEPowerProject. Реализующий класс обязан также реализовать интерфейс IDisposable
    /// </summary>
    /// <typeparam name="T">Тип сущности, для которой применяется паттерн репозитория</typeparam>
    public interface ITNERepository<T> : IDisposable where T : TNEEntityBase
    {
        /// <summary>
        /// Позволяет получить список всех объектов
        /// </summary>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Позволяет получить конкретный объект по Id
        /// </summary>
        Task<T> GetById(int id);
        /// <summary>
        /// Позволяет добавить новый объект
        /// </summary>
        Task<bool> Add(T entity);
        /// <summary>
        /// Позволяет удалить объект по Id
        /// </summary>
        Task<bool> Delete(int id);
        /// <summary>
        /// Позволяет обновить объект по Id
        /// </summary>
        Task<bool> Update(T entity);
        /// <summary>
        /// Позволяет найти объекты по заданному условию
        /// </summary>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Позволяет сохранить изменения в БД
        /// </summary>
        Task SaveChangesAsync();
    }
}
