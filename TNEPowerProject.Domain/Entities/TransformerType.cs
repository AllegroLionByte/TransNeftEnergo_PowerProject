using System.Collections.Generic;
using TNEPowerProject.Domain.Interfaces;

namespace TNEPowerProject.Domain.Entities
{
    /// <summary>
    /// Представляет описание типа трансформатора
    /// </summary>
    public class TransformerType : TNEEntityBase
    {
        /// <summary>
        /// Классифицирует трансформатор по роду работы
        /// </summary>
        public enum TransformerPurpose
        {
            /// <summary>
            /// Указывает, что трансформатор является трансформатором тока
            /// </summary>
            Current = 0,
            /// <summary>
            /// Указывает, что трансформатор является трансформатором напряжения
            /// </summary>
            Voltage = 1
        }
        /// <summary>
        /// Род работы трансформатора
        /// </summary>
        public TransformerPurpose Purpose { get; set; }
        /// <summary>
        /// Описание (название) типа трансформатора
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список трансформаторов тока данного типа
        /// </summary>
        public virtual ICollection<CurrentTransformer> CurrentTransformers { get; set; }
        /// <summary>
        /// Список трансформаторов напряжения данного типа
        /// </summary>
        public virtual ICollection<VoltageTransformer> VoltageTransformers { get; set; }
    }
}
