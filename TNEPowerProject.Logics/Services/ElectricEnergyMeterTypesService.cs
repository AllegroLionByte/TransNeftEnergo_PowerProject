using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeterTypes;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для типов счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMeterTypesService : IElectricEnergyMeterTypesService
    {
        private readonly ElectricEnergyMeterTypesRepository electricEnergyMeterTypesRepository;
        /// <summary>
        /// Представляет реализацию сервиса для типов счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMeterTypesService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для типов счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMeterTypesService(EnergoDBContext dbContext, ILogger logger)
        {
            electricEnergyMeterTypesRepository = new ElectricEnergyMeterTypesRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет добавить новый тип счётчика электрической энергии
        /// </summary>
        public async Task<ElectricEnergyMeterTypeDTO> CreateElectricEnergyMeterType(CreateElectricEnergyMeterTypeDTO createElectricEnergyMeterTypeDTO)
        {
            if (string.IsNullOrWhiteSpace(createElectricEnergyMeterTypeDTO.Description))
            {
                return new ElectricEnergyMeterTypeDTO(RestResponseCode.BadRequest, "Не указано описание (название) типа счётчика электрической энергии.");
            }
            ElectricEnergyMeterType createEEMTResult = await electricEnergyMeterTypesRepository.Add(new ElectricEnergyMeterType()
            {
                Description = createElectricEnergyMeterTypeDTO.Description.Trim()
            });
            if (createEEMTResult == null)
            {
                return new ElectricEnergyMeterTypeDTO(RestResponseCode.InternalServerError, "Произошла ошибка при попытке добавления нового типа счётчика электрической энергии.");
            }
            else
            {
                return new ElectricEnergyMeterTypeDTO(RestResponseCode.Created)
                {
                    Id = createEEMTResult.Id,
                    Description = createEEMTResult.Description
                };
            }
        }
        /// <summary>
        /// Позволяет проверить существование типа счётчика электрической энергии с указанным Id
        /// </summary>
        /// <param name="electricEnergyMeterTypeId">
        /// Id типа счётчика электрической энергии
        /// </param>
        public async Task<ElectricEnergyMeterTypeExistenceDTO> CheckElectricEnergyMeterTypeExists(int electricEnergyMeterTypeId)
        {
            return new ElectricEnergyMeterTypeExistenceDTO(RestResponseCode.OK)
            {
                Exists = await electricEnergyMeterTypesRepository.Exists(x => x.Id == electricEnergyMeterTypeId)
            };
        }
        /// <summary>
        /// Позволяет получить список всех типов счётчиков электрической энергии
        /// </summary>
        public async Task<ElectricEnergyMeterTypesListDTO> GetAllElectricEnergyMeterTypes()
        {
            return new ElectricEnergyMeterTypesListDTO(RestResponseCode.OK)
            {
                ElectricEnergyMeterTypes = (await electricEnergyMeterTypesRepository.GetAll()).Select(x => new ElectricEnergyMeterTypesListDTO.ElectricEnergyMeterTypeListItemDTO()
                {
                    Id = x.Id,
                    Description = x.Description
                }).ToList()
            };
        }
    }
}
