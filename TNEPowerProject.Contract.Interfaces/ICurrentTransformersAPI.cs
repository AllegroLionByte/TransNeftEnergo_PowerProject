using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для трансформаторов тока
    /// </summary>
    public interface ICurrentTransformersAPI
    {
        /// <summary>
        /// Метод для создания нового трансформатора тока
        /// </summary>
        /// <param name="createCurrentTransformerDTO">
        /// DTO для нового трансформатора тока
        /// </param>
        [HttpPost]
        Task<CurrentTransformerDTO> CreateCurrentTransformer(CreateCurrentTransformerDTO createCurrentTransformerDTO);
        /// <summary>
        /// Метод для проверки существования трансформатора тока с указанным Id
        /// </summary>
        /// <param name="currentTransformerId">
        /// Id трансформатора тока
        /// </param>
        [HttpGet("exists")]
        Task<CurrentTransformerExistenceDTO> CheckCurrentTransformerExists(int currentTransformerId);
    }
}
