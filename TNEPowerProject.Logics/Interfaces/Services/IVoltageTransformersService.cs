using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для трансформаторов напряжения
    /// </summary>
    public interface IVoltageTransformersService
    {
        /// <summary>
        /// Позволяет добавить новый трансформатор напряжения
        /// </summary>
        Task<TNEBaseDTO<VoltageTransformerDTO>> CreateVoltageTransformer(CreateVoltageTransformerDTO createVoltageTransformerDTO);
        /// <summary>
        /// Позволяет проверить существование трансформатора напряжения с указанным Id
        /// </summary>
        /// <param name="voltageTransformerId">
        /// Id трансформатора напряжения
        /// </param>
        Task<TNEBaseDTO<VoltageTransformerExistenceDTO>> CheckVoltageTransformerExists(int voltageTransformerId);
    }
}
