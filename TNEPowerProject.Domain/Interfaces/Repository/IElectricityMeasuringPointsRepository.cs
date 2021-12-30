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
        /// Позволяет получить список точек измерения электроэнергии по заданному условию, включив в результат трансформаторы и
        /// счётчики электрической энергии
        /// </summary>
        /// <param name="predicate">
        /// Выражение для конструкции Where
        /// </param>
        Task<IEnumerable<ElectricityMeasuringPoint>> FindWithIncludedEquipment(Expression<Func<ElectricityMeasuringPoint, bool>> predicate);
    }
}
