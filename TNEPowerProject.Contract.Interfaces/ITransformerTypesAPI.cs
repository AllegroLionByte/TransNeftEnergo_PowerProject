using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для типов трансформаторов
    /// </summary>
    public interface ITransformerTypesAPI
    {
        /// <summary>
        /// Метод для проверки существования типа трансформатора с указанным Id
        /// </summary>
        /// <param name="transformerTypeId">
        /// Id типа трансформатора
        /// </param>
        [HttpGet("{transformerTypeId}/exists")]
        [Get("/api/transformertypes/{transformerTypeId}/exists")]
        Task<TNEBaseDTO<TransformerTypeExistenceDTO>> CheckTransformerTypeExists(int transformerTypeId);
        /// <summary>
        /// Метод для создания нового типа трансформатора
        /// </summary>
        /// <param name="createTransformerTypeDTO">
        /// DTO для нового типа трансформатора
        /// </param>
        [HttpPost]
        [Post("/api/transformertypes")]
        Task<TNEBaseDTO<TransformerTypeDTO>> CreateTransformerType([Body] CreateTransformerTypeDTO createTransformerTypeDTO);
        /// <summary>
        /// Метод для получения списка всех типов трансформаторов
        /// </summary>
        [HttpGet]
        [Get("/api/transformertypes")]
        Task<TNEBaseDTO<TransformerTypesListDTO>> GetAll();
    }
}
