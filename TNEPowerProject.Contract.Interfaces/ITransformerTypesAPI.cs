using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для типов трансформаторов
    /// </summary>
    public interface ITransformerTypesAPI
    {
        /// <summary>
        /// Метод для создания нового типа трансформатора
        /// </summary>
        /// <param name="createTransformerTypeDTO">
        /// DTO для нового типа трансформатора
        /// </param>
        [HttpPost]
        Task<TransformerTypeDTO> CreateTransformerType(CreateTransformerTypeDTO createTransformerTypeDTO);
        /// <summary>
        /// Метод для получения списка всех типов трансформаторов
        /// </summary>
        [HttpGet("list")]
        Task<TransformerTypesListDTO> GetAll();
        /// <summary>
        /// Метод для проверки существования типа трансформатора с указанным Id
        /// </summary>
        /// <param name="transformerTypeId">
        /// Id типа трансформатора
        /// </param>
        [HttpGet("exists")]
        Task<TransformerTypeExistenceDTO> CheckTransformerTypeExists(int transformerTypeId);
    }
}
