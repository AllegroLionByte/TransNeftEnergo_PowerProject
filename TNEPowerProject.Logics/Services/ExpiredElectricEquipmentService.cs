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
        public Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<ElectricEnergyMeterDTO>>> GetExpiredElectricEnergyMeters(int consObjId)
        {
            throw new System.NotImplementedException();
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
            electricityMeasuringPointsRepository.FindIncluded(x => x.CurrentTransformer, w => w.ElectricityConsumptionObjectId == consObjId && w.CurrentTransformer.VerificationPeriod < nowDate);
            ElectricityMeasuringPointsRepository m = new ElectricityMeasuringPointsRepository(null, null);


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
        public Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<VoltageTransformerDTO>>> GetExpiredVoltageTransformers(int consObjId)
        {
            throw new System.NotImplementedException();
        }
    }
}
