using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Logics.Interfaces.Services
{
    /// <summary>
    /// Представляет интерфейс сервиса для типов трансформаторов
    /// </summary>
    public interface ITransformerTypesService
    {
        /// <summary>
        /// Позволяет добавить новый тип трансформатора
        /// </summary>
        Task<TransformerTypeDTO> CreateTransformerType(CreateTransformerTypeDTO createTransformerTypeDTO);
        /// <summary>
        /// Позволяет получить список всех типов трансформаторов
        /// </summary>
        Task<TransformerTypesListDTO> GetAllTransformerTypes();
        /// <summary>
        /// Позволяет проверить существование типа трансформатора с указанным Id
        /// </summary>
        /// <param name="transformerTypeId">
        /// Id типа трансформатора
        /// </param>
        Task<TransformerTypeExistenceDTO> CheckTransformerTypeExists(int transformerTypeId);
    }
}
