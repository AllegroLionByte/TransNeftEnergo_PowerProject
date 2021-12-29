using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.ElectricityMeasuringPoints;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для точек измерения электроэнергии
    /// </summary>
    public class ElectricityMeasuringPointsService : IElectricityMeasuringPointsService
    {
        private readonly ElectricityConsumptionObjectsRepository electricityConsumptionObjectsRepository;
        private readonly ElectricityMeasuringPointsRepository electricityMeasuringPointsRepository;
        private readonly ElectricEnergyMetersRepository electricEnergyMetersRepository;
        private readonly CurrentTransformersRepository currentTransformersRepository;
        private readonly VoltageTransformersRepository voltageTransformersRepository;
        /// <summary>
        /// Представляет реализацию сервиса для точек измерения электроэнергии
        /// </summary>
        public ElectricityMeasuringPointsService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для точек измерения электроэнергии
        /// </summary>
        public ElectricityMeasuringPointsService(EnergoDBContext dbContext, ILogger logger)
        {
            electricityConsumptionObjectsRepository = new ElectricityConsumptionObjectsRepository(dbContext, logger);
            electricityMeasuringPointsRepository = new ElectricityMeasuringPointsRepository(dbContext, logger);
            electricEnergyMetersRepository = new ElectricEnergyMetersRepository(dbContext, logger);
            currentTransformersRepository = new CurrentTransformersRepository(dbContext, logger);
            voltageTransformersRepository = new VoltageTransformersRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет проверить существование точки измерения электроэнергии с указанным Id
        /// </summary>
        /// <param name="eMeasPointId">
        /// Id точки измерения электроэнергии
        /// </param>
        public async Task<TNEBaseDTO<ElectricityMeasuringPointExistenceDTO>> CheckElectricityMeasuringPointExists(int eMeasPointId)
        {
            return new TNEBaseDTO<ElectricityMeasuringPointExistenceDTO>(RestResponseCode.OK)
            {
                Result = new ElectricityMeasuringPointExistenceDTO()
                {
                    Exists = await electricityMeasuringPointsRepository.Exists(x => x.Id == eMeasPointId)
                }
            };
        }
        /// <summary>
        /// Позволяет добавить новую точку измерения электроэнергии
        /// </summary>
        /// <param name="createElectricityMeasuringPointDTO">
        /// DTO для новой точки измерения электроэнергии
        /// </param>
        public async Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> CreateElectricityMeasuringPoint(CreateElectricityMeasuringPointDTO createElectricityMeasuringPointDTO)
        {
            if (string.IsNullOrWhiteSpace(createElectricityMeasuringPointDTO.Name))
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Неправильно указано наименование точки измерения.");

            ElectricityConsumptionObject electricityConsumptionObject = await electricityConsumptionObjectsRepository.GetById(createElectricityMeasuringPointDTO.ElectricityConsumptionObjectId);
            if (electricityConsumptionObject == null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный объект потребления не существует.");

            ElectricEnergyMeter electricEnergyMeter = await electricEnergyMetersRepository.GetById(createElectricityMeasuringPointDTO.ElectricEnergyMeterId);
            if (electricEnergyMeter == null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный счётчик электрической энергии не существует.");
            if (electricEnergyMeter.MeasuringPoint != null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный счётчик электрической энергии уже используется в другой точке измеренеия электроэнергии.");

            CurrentTransformer currentTransformer = await currentTransformersRepository.GetById(createElectricityMeasuringPointDTO.CurrentTransformerId);
            if (currentTransformer == null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный трансформатор тока не существует.");
            if (currentTransformer.MeasuringPoint != null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный трансформатор тока уже используется в другой точке измеренеия электроэнергии.");

            VoltageTransformer voltageTransformer = await voltageTransformersRepository.GetById(createElectricityMeasuringPointDTO.VoltageTransformerId);
            if (voltageTransformer == null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный трансформатор напряжения не существует.");
            if (voltageTransformer.MeasuringPoint != null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.BadRequest, "Указаный трансформатор напряжения уже используется в другой точке измеренеия электроэнергии.");

            ElectricityMeasuringPoint createElectricityMeasuringPoint = await electricityMeasuringPointsRepository.Add(new ElectricityMeasuringPoint()
            {
                Name = createElectricityMeasuringPointDTO.Name.Trim(),
                ElectricEnergyMeterId = electricEnergyMeter.Id,
                CurrentTransformerId = currentTransformer.Id,
                VoltageTransformerId = voltageTransformer.Id,
                ElectricityConsumptionObjectId = createElectricityMeasuringPointDTO.ElectricityConsumptionObjectId
            });
            if (createElectricityMeasuringPoint == null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.InternalServerError, "Произошла ошибка при попытке добавления новой точки измерения электроэнергии.");
            else
            {
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.Created)
                {
                    Result = new ElectricityMeasuringPointDTO()
                    {
                        Id = createElectricityMeasuringPoint.Id,
                        Name = createElectricityMeasuringPoint.Name,
                        ElectricEnergyMeterId = electricEnergyMeter.Id,
                        ElectricEnergyMeterNumber = electricEnergyMeter.Number,
                        CurrentTransformerId = currentTransformer.Id,
                        CurrentTransformerNumber = currentTransformer.Number,
                        VoltageTransformerId = voltageTransformer.Id,
                        VoltageTransformerNumber = voltageTransformer.Number,
                        ElectricityConsumptionObjectId = createElectricityMeasuringPointDTO.ElectricityConsumptionObjectId
                    }
                };
            }
        }
        /// <summary>
        /// Позволяет получить точку измерения электроэнергии по указанному Id
        /// </summary>
        /// <param name="eMeasPointId">
        /// Id точки измерения электроэнергии
        /// </param>
        public async Task<TNEBaseDTO<ElectricityMeasuringPointDTO>> GetElectricityMeasuringPoint(int eMeasPointId)
        {
            ElectricityMeasuringPoint electricityMeasuringPoint = await electricityMeasuringPointsRepository.GetById(eMeasPointId);
            if (electricityMeasuringPoint == null)
                return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.NotFound);
            return new TNEBaseDTO<ElectricityMeasuringPointDTO>(RestResponseCode.OK)
            {
                Result = new ElectricityMeasuringPointDTO()
                {
                    Id = electricityMeasuringPoint.Id,
                    Name = electricityMeasuringPoint.Name,
                    ElectricEnergyMeterId = electricityMeasuringPoint.ElectricEnergyMeterId,
                    ElectricEnergyMeterNumber = electricityMeasuringPoint.ElectricEnergyMeter.Number,
                    CurrentTransformerId = electricityMeasuringPoint.CurrentTransformerId,
                    CurrentTransformerNumber = electricityMeasuringPoint.CurrentTransformer.Number,
                    VoltageTransformerId = electricityMeasuringPoint.VoltageTransformerId,
                    VoltageTransformerNumber = electricityMeasuringPoint.VoltageTransformer.Number,
                    ElectricityConsumptionObjectId = electricityMeasuringPoint.ElectricityConsumptionObjectId
                }
            };
        }
    }
}
