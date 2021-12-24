using System.Collections.Generic;
using TNEPowerProject.Contract.Enums;

namespace TNEPowerProject.Contract.DTO.Transformers
{
    /// <summary>
    /// Представляет DTO для списка типов трансформаторов
    /// </summary>
    public class TransformerTypesListDTO : TNERestfulBaseDTO
    {
        /// <summary>
        /// Представляет DTO для списка типов трансформаторов
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        public TransformerTypesListDTO(RestResponseCode responseCode) : this(responseCode, "") { }
        /// <summary>
        /// Представляет DTO для списка типов трансформаторов
        /// </summary>
        /// <param name="responseCode">
        /// Статус операции из перечисления TNEPowerProject.Contract.Enums.RestResponseCode
        /// </param>
        /// <param name="message">
        /// Сообщение о статусе операции
        /// </param>
        public TransformerTypesListDTO(RestResponseCode responseCode, string message) : base(responseCode, message)
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
