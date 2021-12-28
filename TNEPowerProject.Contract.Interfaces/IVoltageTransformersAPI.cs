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
        /// Метод для создания нового трансформатора напряжения
        /// </summary>
        /// <param name="createVoltageTransformerDTO">
        /// DTO для нового трансформатора напряжения
        /// </param>
        [HttpPost]
        Task<TNEBaseDTO<VoltageTransformerDTO>> CreateVoltageTransformer(CreateVoltageTransformerDTO createVoltageTransformerDTO);
        /// <summary>
        /// Метод для проверки существования трансформатора напряжения с указанным Id
        /// </summary>
        /// <param name="voltageTransformerId">
        /// Id трансформатора напряжения
        /// </param>
        [HttpGet("exists")]
        Task<TNEBaseDTO<VoltageTransformerExistenceDTO>> CheckVoltageTransformerExists(int voltageTransformerId);
    }
}
