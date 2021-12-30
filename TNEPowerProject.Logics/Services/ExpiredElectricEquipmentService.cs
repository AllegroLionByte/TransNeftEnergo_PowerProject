using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Domain.Interfaces.Repository;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO.ExpiredElectricEquipment;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для получения списков объектов электрической инфраструктуры (трансформаторов и
    /// счётчиков электрической энергии) с истёкшими сроками поверки
    /// </summary>
    public class ExpiredElectricEquipmentService : IExpiredElectricEquipmentService
    {
        private readonly IElectricityConsumptionObjectsRepository electricityConsumptionObjectsRepository;
        private readonly IElectricityMeasuringPointsRepository electricityMeasuringPointsRepository;
        /// <summary>
        /// Представляет реализацию сервиса для получения списков объектов электрической инфраструктуры (трансформаторов и
        /// счётчиков электрической энергии) с истёкшими сроками поверки
        /// </summary>
        public ExpiredElectricEquipmentService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для получения списков объектов электрической инфраструктуры (трансформаторов и
        /// счётчиков электрической энергии) с истёкшими сроками поверки
        /// </summary>
        public ExpiredElectricEquipmentService(EnergoDBContext dbContext, ILogger logger)
        {
            electricityConsumptionObjectsRepository = new ElectricityConsumptionObjectsRepository(dbContext, logger);
            electricityMeasuringPointsRepository = new ElectricityMeasuringPointsRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет получить список всех счётчиков электрической энергии с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        public async Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>> GetExpiredElectricEnergyMeters(int consObjId)
        {
            ElectricityConsumptionObject eConsObj = await electricityConsumptionObjectsRepository.GetById(consObjId);
            if (eConsObj == null)
                return new TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>(RestResponseCode.NotFound,
                    "Объект потребления для заданного Id не найден.");

            ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO> result = new ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>()
            {
                ConsumptionObjectId = consObjId,
                ConsumptionObjectName = eConsObj.Name
            };

            DateTime nowDate = DateTime.Now.Date;
            result.ExpiredElectricEquipment = new List<ElectricEnergyMeterDTO>((await electricityMeasuringPointsRepository
                .FindWithIncludedEquipment(w => w.ElectricityConsumptionObjectId == consObjId && w.ElectricEnergyMeter.VerificationPeriod < nowDate))
                .Select(x => new ElectricEnergyMeterDTO()
                {
                    Id = x.CurrentTransformer.Id,
                    Number = x.CurrentTransformer.Number,
                    EEMeterTypeId = x.ElectricEnergyMeter.Id,
                    EEMeterTypeDescription = x.ElectricEnergyMeter.EEMeterType.Description,
                    VerificationDate = x.CurrentTransformer.VerificationDate,
                    VerificationPeriod = x.CurrentTransformer.VerificationPeriod
                }));

            return new TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>(RestResponseCode.OK)
            {
                Result = result
            };
        }
        /// <summary>
        /// Позволяет получить список всех трансформаторов тока с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        public async Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>> GetExpiredCurrentTransformers(int consObjId)
        {
            ElectricityConsumptionObject eConsObj = await electricityConsumptionObjectsRepository.GetById(consObjId);
            if (eConsObj == null)
                return new TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>(RestResponseCode.NotFound,
                    "Объект потребления для заданного Id не найден.");

            ExpiredElectricEquipmentListDTO<CurrentTransformerDTO> result = new ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>()
            {
                ConsumptionObjectId = consObjId,
                ConsumptionObjectName = eConsObj.Name
            };

            DateTime nowDate = DateTime.Now.Date;
            result.ExpiredElectricEquipment = new List<CurrentTransformerDTO>((await electricityMeasuringPointsRepository
                .FindWithIncludedEquipment(w => w.ElectricityConsumptionObjectId == consObjId && w.CurrentTransformer.VerificationPeriod < nowDate))
                .Select(x => new CurrentTransformerDTO()
                {
                    Id = x.CurrentTransformer.Id,
                    Number = x.CurrentTransformer.Number,
                    TransformationRatio = x.CurrentTransformer.TransformationRatio,
                    TransformerTypeId = x.CurrentTransformer.TransformerTypeId,
                    TransformerTypeDescription = x.CurrentTransformer.TransformerType.Description,
                    VerificationDate = x.CurrentTransformer.VerificationDate,
                    VerificationPeriod = x.CurrentTransformer.VerificationPeriod
                }));

            return new TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>(RestResponseCode.OK)
            {
                Result = result
            };
        }
        /// <summary>
        /// Позволяет получить список всех трансформаторов напряжения с просроченным сроком поверки
        /// для указанного объекта потребления
        /// </summary>
        /// <param name="consObjId">
        /// Id объекта потребления, для которого выполняется поиск
        /// </param>
        public async Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>> GetExpiredVoltageTransformers(int consObjId)
        {
            ElectricityConsumptionObject eConsObj = await electricityConsumptionObjectsRepository.GetById(consObjId);
            if (eConsObj == null)
                return new TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>(RestResponseCode.NotFound,
                    "Объект потребления для заданного Id не найден.");

            ExpiredElectricEquipmentListDTO<VoltageTransformerDTO> result = new ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>()
            {
                ConsumptionObjectId = consObjId,
                ConsumptionObjectName = eConsObj.Name
            };

            DateTime nowDate = DateTime.Now.Date;
            result.ExpiredElectricEquipment = new List<VoltageTransformerDTO>((await electricityMeasuringPointsRepository
                .FindWithIncludedEquipment(w => w.ElectricityConsumptionObjectId == consObjId && w.CurrentTransformer.VerificationPeriod < nowDate))
                .Select(x => new VoltageTransformerDTO()
                {
                    Id = x.CurrentTransformer.Id,
                    Number = x.CurrentTransformer.Number,
                    TransformationRatio = x.CurrentTransformer.TransformationRatio,
                    TransformerTypeId = x.CurrentTransformer.TransformerTypeId,
                    TransformerTypeDescription = x.CurrentTransformer.TransformerType.Description,
                    VerificationDate = x.CurrentTransformer.VerificationDate,
                    VerificationPeriod = x.CurrentTransformer.VerificationPeriod
                }));

            return new TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>(RestResponseCode.OK)
            {
                Result = result
            };
        }
    }
}
