using System.Collections.Generic;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для списка типов трансформаторов
    /// </summary>
    public class TransformerTypesListDTO : ITNEDTO
    {
        /// <summary>
        /// Представляет DTO для списка типов трансформаторов
        /// </summary>
        public TransformerTypesListDTO()
        {
            TransformerTypes = new List<TransformerTypeListItemDTO>();
        }
        /// <summary>
        /// Список типов трансформаторов
        /// </summary>
        public ICollection<TransformerTypeListItemDTO> TransformerTypes { get; set; }
        /// <summary>
        /// DTO элемент списка для типов трансформаторов
        /// </summary>
        public class TransformerTypeListItemDTO
        {
            /// <summary>
            /// Уникальный идентификатор типа трансформатора
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// Род работы трансформатора
            /// </summary>
            public int TransformerPurpose { get; set; }
            /// <summary>
            /// Описание (название) типа трансформатора
            /// </summary>
            public string Description { get; set; }
        }
    }
}
