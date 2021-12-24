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
    }
}
