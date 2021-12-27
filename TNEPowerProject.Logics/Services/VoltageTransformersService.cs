using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Infrastructure.Database.EFCore;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для трансформаторов напряжения
    /// </summary>
    public class VoltageTransformersService : IVoltageTransformersService
    {
        private readonly VoltageTransformersRepository voltageTransformersRepository;
        private readonly TransformerTypesRepository transformerTypesRepository;
        /// <summary>
        /// Представляет реализацию сервиса для трансформаторов напряжения
        /// </summary>
        public VoltageTransformersService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для трансформаторов напряжения
        /// </summary>
        public VoltageTransformersService(EnergoDBContext dbContext, ILogger logger)
        {
            voltageTransformersRepository = new VoltageTransformersRepository(dbContext, logger);
            transformerTypesRepository = new TransformerTypesRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет проверить существование трансформатора напряжения с указанным Id
        /// </summary>
        /// <param name="voltageTransformerId">
        /// Id трансформатора напряжения
        /// </param>
        public async Task<VoltageTransformerExistenceDTO> CheckVoltageTransformerExists(int voltageTransformerId)
        {
            return new VoltageTransformerExistenceDTO(RestResponseCode.OK)
            {
                Exists = await voltageTransformersRepository.Exists(x => x.Id == voltageTransformerId)
            };
        }
        /// <summary>
        /// Позволяет добавить новый трансформатор напряжения
        /// </summary>
        public async Task<VoltageTransformerDTO> CreateVoltageTransformer(CreateVoltageTransformerDTO createVoltageTransformerDTO)
        {
            if (createVoltageTransformerDTO.Number < 0)
            {
                return new VoltageTransformerDTO(RestResponseCode.BadRequest, "Неправильно указан номер трансформатора.");
            }
            if (createVoltageTransformerDTO.TransformationRatio <= 0)
            {
                return new VoltageTransformerDTO(RestResponseCode.BadRequest, "Неправильно указан коэфициент трансформации по напряжению.");
            }
            else if (createVoltageTransformerDTO.VerificationDate > createVoltageTransformerDTO.VerificationPeriod)
            {
                return new VoltageTransformerDTO(RestResponseCode.BadRequest, "Срок поверки не может находится после даты поверки.");
            }
            TransformerType transformerType = await transformerTypesRepository.GetById(createVoltageTransformerDTO.TransformerTypeId);
            if (transformerType == null)
            {
                return new VoltageTransformerDTO(RestResponseCode.BadRequest, "Указаный тип трансформатора не найден, либо не правильно указан его Id.");
            }
            else if (transformerType.Purpose != TransformerType.TransformerPurpose.Voltage)
            {
                return new VoltageTransformerDTO(RestResponseCode.BadRequest, "Указаный тип трансформатора не относится к трансформаторам напряжения.");
            }
            VoltageTransformer createVTransformer = await voltageTransformersRepository.Add(new VoltageTransformer()
            {
                Number = createVoltageTransformerDTO.Number,
                TransformationRatio = createVoltageTransformerDTO.TransformationRatio,
                VerificationDate = createVoltageTransformerDTO.VerificationDate,
                VerificationPeriod = createVoltageTransformerDTO.VerificationPeriod,
                TransformerTypeId = createVoltageTransformerDTO.TransformerTypeId
            });
            if (createVTransformer == null)
            {
                return new VoltageTransformerDTO(RestResponseCode.InternalServerError, "Произошла ошибка при попытке добавления нового трансформатора напряжения.");
            }
            else
            {
                return new VoltageTransformerDTO(RestResponseCode.Created)
                {
                    Id = createVTransformer.Id,
                    Number = createVTransformer.Number,
                    TransformationRatio = createVTransformer.TransformationRatio,
                    TransformerTypeId = createVTransformer.TransformerTypeId,
                    TransformerTypeDescription = transformerType.Description,
                    VerificationDate = createVTransformer.VerificationDate,
                    VerificationPeriod = createVTransformer.VerificationPeriod
                };
            }
        }
    }
}
