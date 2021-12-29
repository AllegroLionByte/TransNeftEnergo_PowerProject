using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для объектов потребления
    /// </summary>
    public class ElectricityConsumptionObjectsService : IElectricityConsumptionObjectsService
    {
        private readonly ElectricityConsumptionObjectsRepository electricityConsumptionObjectsRepository;
        /// <summary>
        /// Представляет реализацию сервиса для объектов потребления
        /// </summary>
        public ElectricityConsumptionObjectsService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для объектов потребления
        /// </summary>
        public ElectricityConsumptionObjectsService(EnergoDBContext dbContext, ILogger logger)
        {
            electricityConsumptionObjectsRepository = new ElectricityConsumptionObjectsRepository(dbContext, logger);
        }
        /// <summary>
        /// Метод для получения списка всех объектов потребления
        /// </summary>
        public async Task<TNEBaseDTO<ElectricityConsumptionObjectsListDTO>> GetAllElectricityConsumptionObjects()
        {
            return new TNEBaseDTO<ElectricityConsumptionObjectsListDTO>(RestResponseCode.OK)
            {
                Result = new ElectricityConsumptionObjectsListDTO()
                {
                    ElectricityConsumptionObjects = (await electricityConsumptionObjectsRepository.GetAll()).Select(x => new ElectricityConsumptionObjectsListDTO.ElectricityConsumptionObjectListItemDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SubOrganizationId = x.SubOrganizationId,
                        SubOrganizationName = x.SubOrganization.Name
                    }).ToList()
                }
            };
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
        public Task<TNEBaseDTO<ExpiredElectricEquipmentListDTO<CurrentTransformerDTO>>> GetExpiredCurrentTransformers(int consObjId)
        {
            throw new System.NotImplementedException();
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
