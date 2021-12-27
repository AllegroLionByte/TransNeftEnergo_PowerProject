using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TNEPowerProject.Domain.Abstract;
using TNEPowerProject.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TNEPowerProject.Infrastructure.Repository
{
    /// <summary>
    /// Представляет базовый класс репозитория с общей реализацией в проекте TNEPowerProject
    /// </summary>
    public class TNERepository<T> : ITNERepository<T> where T : TNEEntityBase
    {
        /// <summary>
        /// Объект контекста для БД
        /// </summary>
        protected readonly DbContext dbContext;
        /// <summary>
        /// Объект логгирования.
        /// </summary>
        /// <remarks>
        /// [Осторожно] Может быть null!
        /// </remarks>
        protected readonly ILogger logger;
        private bool _disposed = false;
        /// <summary>
        /// Представляет базовый класс репозитория с общей реализацией в проекте TNEPowerProject
        /// </summary>
        public TNERepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Представляет базовый класс репозитория с поддержкой логгирования с общей реализацией в проекте TNEPowerProject
        /// </summary>
        public TNERepository(DbContext dbContext, ILogger logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        /// <summary>
        /// Позволяет добавить новый объект
        /// </summary>
        public virtual async Task<T> Add(T entity)
        {
            try
            {
                EntityEntry<T> eE = await dbContext.Set<T>().AddAsync(entity);
                await SaveChangesAsync();
                return eE.Entity;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during creating element {Entity}.", GetType(), typeof(T));
                return null;
            }
        }
        /// <summary>
        /// Позволяет удалить объект по Id
        /// </summary>
        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                T entity = await GetById(id);
                if (entity == null)
                    return false;
                dbContext.Set<T>().Remove(entity);
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during deleting element {Entity} by id: {Id}.", GetType(), typeof(T), id);
                return false;
            }
        }
        /// <summary>
        /// Позволяет найти объекты по заданному условию
        /// </summary>
        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dbContext.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during finding elements {Entity} by {Pred}.", GetType(), typeof(T), predicate);
                return new List<T>();
            }
        }
        /// <summary>
        /// Позволяет получить список всех объектов
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting all elements {Entity}.", GetType(), typeof(T));
                return new List<T>();
            }
        }
        /// <summary>
        /// Позволяет получить объект по его Id
        /// </summary>
        public virtual async Task<T> GetById(int id)
        {
            try
            {
                return await dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during getting element {Entity} by id: {Id}.", GetType(), typeof(T), id);
                return default(T);
            }
        }
        /// <summary>
        /// Позволяет узнать, присутствует ли данный объект в БД
        /// </summary>
        public virtual async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await dbContext.Set<T>().AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "{Repo}: error during checking element existance {Entity} by {Pred}.", GetType(), typeof(T), predicate);
                return false;
            }
        }
        /// <summary>
        /// Позволяет обновить объект по Id
        /// </summary>
        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Позволяет сохранить изменения в БД
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Используется для завершающих операций при удалении контекста из памяти
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        /// <summary>
        /// Используется для завершающих операций при удалении контекста из памяти
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
