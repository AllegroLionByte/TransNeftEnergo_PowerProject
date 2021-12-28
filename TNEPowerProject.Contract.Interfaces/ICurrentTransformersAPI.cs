using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для трансформаторов тока
    /// </summary>
    public interface ICurrentTransformersAPI
    {
        /// <summary>
        /// Метод для проверки существования трансформатора тока с указанным Id
        /// </summary>
        /// <param name="currentTransformerId">
        /// Id трансформатора тока
        /// </param>
        [HttpGet("{currentTransformerId}/exists")]
        [Get("/api/currenttransformers/{currentTransformerId}/exists")]
        Task<TNEBaseDTO<CurrentTransformerExistenceDTO>> CheckCurrentTransformerExists(int currentTransformerId);
        /// <summary>
        /// Метод для создания нового трансформатора тока
        /// </summary>
        /// <param name="createCurrentTransformerDTO">
        /// DTO для нового трансформатора тока
        /// </param>
        [HttpPost]
        [Post("/api/currenttransformers")]
        Task<TNEBaseDTO<CurrentTransformerDTO>> CreateCurrentTransformer([Body] CreateCurrentTransformerDTO createCurrentTransformerDTO);
    }
}
