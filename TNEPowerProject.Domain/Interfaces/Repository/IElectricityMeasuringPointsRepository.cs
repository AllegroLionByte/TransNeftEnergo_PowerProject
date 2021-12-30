using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using TNEPowerProject.Domain.Entities;

namespace TNEPowerProject.Domain.Interfaces.Repository
{
    /// <summary>
    /// Представляет интерфейс для репозитория точек измерения электроэнергии
    /// </summary>
    public interface IElectricityMeasuringPointsRepository : ITNERepository<ElectricityMeasuringPoint>
    {
        /// <summary>
        /// Позволяет получить список точек измерения электроэнергии по заданному условию, включив в результат дополнительные данные
        /// </summary>
        /// <param name="includeExpr">
        /// Выражение, которое будет включено в конструкцию Include. В данном месте следует указать объект,
        /// который будет включён в выдаваемый ответ для сущности ElectricityMeasuringPoint
        /// </param>
        /// <param name="predicate">
        /// Выражение для конструкции Where
        /// </param>
        Task<IEnumerable<ElectricityMeasuringPoint>> FindIncluded(Expression<Func<ElectricityMeasuringPoint, object>> includeExpr, Expression<Func<ElectricityMeasuringPoint, bool>> predicate);
    }
}
