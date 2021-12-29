using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Contract.DTO;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO.ElectricityConsumptionObjects;

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
    }
}
