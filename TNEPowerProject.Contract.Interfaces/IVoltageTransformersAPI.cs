using Refit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для трансформаторов напряжения
    /// </summary>
    public interface IVoltageTransformersAPI
    {
        /// <summary>
        /// Метод для проверки существования трансформатора напряжения с указанным Id
        /// </summary>
        /// <param name="voltageTransformerId">
        /// Id трансформатора напряжения
        /// </param>
        [HttpGet("{voltageTransformerId}/exists")]
        [Get("/api/voltagetransformers/{voltageTransformerId}/exists")]
        Task<TNEBaseDTO<VoltageTransformerExistenceDTO>> CheckVoltageTransformerExists(int voltageTransformerId);
        /// <summary>
        /// Метод для создания нового трансформатора напряжения
        /// </summary>
        /// <param name="createVoltageTransformerDTO">
        /// DTO для нового трансформатора напряжения
        /// </param>
        [HttpPost]
        [Post("/api/voltagetransformers")]
        Task<TNEBaseDTO<VoltageTransformerDTO>> CreateVoltageTransformer([Body] CreateVoltageTransformerDTO createVoltageTransformerDTO);
    }
}
