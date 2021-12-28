using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для трансформаторов тока
    /// </summary>
    public interface ICurrentTransformersService
    {
        /// <summary>
        /// Позволяет добавить новый трансформатор тока
        /// </summary>
        Task<TNEBaseDTO<CurrentTransformerDTO>> CreateCurrentTransformer(CreateCurrentTransformerDTO createCurrentTransformerDTO);
        /// <summary>
        /// Позволяет проверить существование трансформатора тока с указанным Id
        /// </summary>
        /// <param name="currentTransformerId">
        /// Id трансформатора тока
        /// </param>
        Task<TNEBaseDTO<CurrentTransformerExistenceDTO>> CheckCurrentTransformerExists(int currentTransformerId);
    }
}
