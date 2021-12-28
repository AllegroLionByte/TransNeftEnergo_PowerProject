using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для типов трансформаторов
    /// </summary>
    public class TransformerTypesService : ITransformerTypesService
    {
        private readonly TransformerTypesRepository transformerTypesRepository;
        /// <summary>
        /// Представляет реализацию сервиса для типов трансформаторов
        /// </summary>
        public TransformerTypesService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для типов трансформаторов
        /// </summary>
        public TransformerTypesService(EnergoDBContext dbContext, ILogger logger)
        {
            transformerTypesRepository = new TransformerTypesRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет добавить новый тип трансформатора
        /// </summary>
        /// <param name="createTransformerTypeDTO">Сущность, описывающая новый тип трансформатора</param>
        public async Task<TNEBaseDTO<TransformerTypeDTO>> CreateTransformerType(CreateTransformerTypeDTO createTransformerTypeDTO)
        {
            if (string.IsNullOrWhiteSpace(createTransformerTypeDTO.Description))
            {
                return new TNEBaseDTO<TransformerTypeDTO>(RestResponseCode.BadRequest, "Не указано описание (название) типа трансформатора.");
            }
            else if (!Enum.IsDefined(typeof(TransformerType.TransformerPurpose), createTransformerTypeDTO.TransformerPurpose))
            {
                return new TNEBaseDTO<TransformerTypeDTO>(RestResponseCode.BadRequest, "Указан неправильный род работы трансформатора.");
            }
            TransformerType createTTResult = await transformerTypesRepository.Add(new TransformerType()
            {
                Description = createTransformerTypeDTO.Description.Trim(),
                Purpose = (TransformerType.TransformerPurpose)createTransformerTypeDTO.TransformerPurpose
            });
            if (createTTResult == null)
            {
                return new TNEBaseDTO<TransformerTypeDTO>(RestResponseCode.InternalServerError, "Произошла ошибка при попытке добавления нового типа трансформатора.");
            }
            else
            {
                return new TNEBaseDTO<TransformerTypeDTO>(RestResponseCode.Created)
                {
                    Result = new TransformerTypeDTO()
                    {
                        Description = createTTResult.Description,
                        Id = createTTResult.Id,
                        TransformerPurpose = (int)createTTResult.Purpose
                    }
                };
            }
        }
        /// <summary>
        /// Метод для проверки существования типа трансформатора с указанным Id
        /// </summary>
        /// <param name="transformerTypeId">
        /// Id типа трансформатора
        /// </param>
        public async Task<TNEBaseDTO<TransformerTypeExistenceDTO>> CheckTransformerTypeExists(int transformerTypeId)
        {
            return new TNEBaseDTO<TransformerTypeExistenceDTO>(RestResponseCode.OK)
            {
                Result = new TransformerTypeExistenceDTO()
                {
                    Exists = await transformerTypesRepository.Exists(x => x.Id == transformerTypeId)
                }
            };
        }
        /// <summary>
        /// Позволяет получить список всех типов трансформаторов
        /// </summary>
        public async Task<TNEBaseDTO<TransformerTypesListDTO>> GetAllTransformerTypes()
        {
            return new TNEBaseDTO<TransformerTypesListDTO>(RestResponseCode.OK)
            {
                Result = new TransformerTypesListDTO()
                {
                    TransformerTypes = (await transformerTypesRepository.GetAll()).Select(x => new TransformerTypesListDTO.TransformerTypeListItemDTO()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        TransformerPurpose = (int)x.Purpose
                    }).ToList()
                }
            };
        }
    }
}
