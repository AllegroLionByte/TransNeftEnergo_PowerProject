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
    /// Представляет реализацию сервиса для трансформаторов тока
    /// </summary>
    public class CurrentTransformersService : ICurrentTransformersService
    {
        private readonly CurrentTransformersRepository currentTransformersRepository;
        private readonly TransformerTypesRepository transformerTypesRepository;
        /// <summary>
        /// Представляет реализацию сервиса для трансформаторов тока
        /// </summary>
        public CurrentTransformersService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для трансформаторов тока
        /// </summary>
        public CurrentTransformersService(EnergoDBContext dbContext, ILogger logger)
        {
            currentTransformersRepository = new CurrentTransformersRepository(dbContext, logger);
            transformerTypesRepository = new TransformerTypesRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет проверить существование трансформатора тока с указанным Id
        /// </summary>
        /// <param name="currentTransformerId">
        /// Id трансформатора тока
        /// </param>
        public async Task<TNEBaseDTO<CurrentTransformerExistenceDTO>> CheckCurrentTransformerExists(int currentTransformerId)
        {
            return new TNEBaseDTO<CurrentTransformerExistenceDTO>(RestResponseCode.OK)
            {
                Result = new CurrentTransformerExistenceDTO()
                {
                    Exists = await currentTransformersRepository.Exists(x => x.Id == currentTransformerId)
                }
            };
        }
        /// <summary>
        /// Позволяет добавить новый трансформатор тока
        /// </summary>
        public async Task<TNEBaseDTO<CurrentTransformerDTO>> CreateCurrentTransformer(CreateCurrentTransformerDTO createCurrentTransformerDTO)
        {
            if (createCurrentTransformerDTO.Number < 0)
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.BadRequest, "Неправильно указан номер трансформатора.");
            }
            if (createCurrentTransformerDTO.TransformationRatio <= 0)
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.BadRequest, "Неправильно указан коэфициент трансформации по току.");
            }
            else if (createCurrentTransformerDTO.VerificationDate > createCurrentTransformerDTO.VerificationPeriod)
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.BadRequest, "Срок поверки не может находится после даты поверки.");
            }
            TransformerType transformerType = await transformerTypesRepository.GetById(createCurrentTransformerDTO.TransformerTypeId);
            if (transformerType == null)
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.BadRequest, "Указаный тип трансформатора не найден, либо не правильно указан его Id.");
            }
            else if (transformerType.Purpose != TransformerType.TransformerPurpose.Current)
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.BadRequest, "Указаный тип трансформатора не относится к трансформаторам тока.");
            }
            CurrentTransformer createCTransformer = await currentTransformersRepository.Add(new CurrentTransformer()
            {
                Number = createCurrentTransformerDTO.Number,
                TransformationRatio = createCurrentTransformerDTO.TransformationRatio,
                VerificationDate = createCurrentTransformerDTO.VerificationDate,
                VerificationPeriod = createCurrentTransformerDTO.VerificationPeriod,
                TransformerTypeId = createCurrentTransformerDTO.TransformerTypeId
            });
            if (createCTransformer == null)
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.InternalServerError, "Произошла ошибка при попытке добавления нового трансформатора тока.");
            }
            else
            {
                return new TNEBaseDTO<CurrentTransformerDTO>(RestResponseCode.Created)
                {
                    Result = new CurrentTransformerDTO()
                    {
                        Id = createCTransformer.Id,
                        Number = createCTransformer.Number,
                        TransformationRatio = createCTransformer.TransformationRatio,
                        TransformerTypeId = createCTransformer.TransformerTypeId,
                        TransformerTypeDescription = transformerType.Description,
                        VerificationDate = createCTransformer.VerificationDate,
                        VerificationPeriod = createCTransformer.VerificationPeriod
                    }
                };
            }
        }
    }
}
