﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Infrastructure.Database.EFCore;
using TNEPowerProject.Contract.DTO.ElectricEnergyMeters;
using TNEPowerProject.Contract.DTO;

namespace TNEPowerProject.Logics.Services
{
    /// <summary>
    /// Представляет реализацию сервиса для счётчиков электрической энергии
    /// </summary>
    public class ElectricEnergyMetersService : IElectricEnergyMetersService
    {
        private readonly ElectricEnergyMetersRepository electricEnergyMetersRepository;
        private readonly ElectricEnergyMeterTypesRepository electricEnergyMeterTypesRepository;
        /// <summary>
        /// Представляет реализацию сервиса для счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMetersService(EnergoDBContext dbContext) : this(dbContext, null) { }
        /// <summary>
        /// Представляет реализацию сервиса для счётчиков электрической энергии
        /// </summary>
        public ElectricEnergyMetersService(EnergoDBContext dbContext, ILogger logger)
        {
            electricEnergyMetersRepository = new ElectricEnergyMetersRepository(dbContext, logger);
            electricEnergyMeterTypesRepository = new ElectricEnergyMeterTypesRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет проверить существование счётчика электрической энергии с указанным Id
        /// </summary>
        /// <param name="electricEnergyMeterId">
        /// Id счётчика электрической энергии
        /// </param>
        public async Task<TNEBaseDTO<ElectricEnergyMeterExistenceDTO>> CheckElectricEnergyMeterExists(int electricEnergyMeterId)
        {
            return new TNEBaseDTO<ElectricEnergyMeterExistenceDTO>(RestResponseCode.OK)
            {
                Result = new ElectricEnergyMeterExistenceDTO()
                {
                    Exists = await electricEnergyMetersRepository.Exists(x => x.Id == electricEnergyMeterId)
                }
            };
        }
        /// <summary>
        /// Позволяет добавить новый счётчик электрической энергии
        /// </summary>
        public async Task<TNEBaseDTO<ElectricEnergyMeterDTO>> CreateElectricEnergyMeter(CreateElectricEnergyMeterDTO createElectricEnergyMeterDTO)
        {
            if (createElectricEnergyMeterDTO.Number < 0)
            {
                return new TNEBaseDTO<ElectricEnergyMeterDTO>(RestResponseCode.BadRequest, "Неправильно указан номер счётчика.");
            }
            else if (createElectricEnergyMeterDTO.VerificationDate > createElectricEnergyMeterDTO.VerificationPeriod)
            {
                return new TNEBaseDTO<ElectricEnergyMeterDTO>(RestResponseCode.BadRequest, "Срок поверки не может находится после даты поверки.");
            }
            ElectricEnergyMeterType eEMType = await electricEnergyMeterTypesRepository.GetById(createElectricEnergyMeterDTO.EEMeterTypeId);
            if (eEMType == null)
            {
                return new TNEBaseDTO<ElectricEnergyMeterDTO>(RestResponseCode.BadRequest, "Указаный тип счётчика электрической энергии не найден, либо не правильно указан его Id.");
            }
            ElectricEnergyMeter createEEMResult = await electricEnergyMetersRepository.Add(new ElectricEnergyMeter()
            {
                Number = createElectricEnergyMeterDTO.Number,
                VerificationDate = createElectricEnergyMeterDTO.VerificationDate,
                VerificationPeriod = createElectricEnergyMeterDTO.VerificationPeriod,
                EEMeterTypeId = createElectricEnergyMeterDTO.EEMeterTypeId
            });
            if (createEEMResult == null)
            {
                return new TNEBaseDTO<ElectricEnergyMeterDTO>(RestResponseCode.InternalServerError, "Произошла ошибка при попытке добавления нового счётчика электрической энергии.");
            }
            else
            {
                return new TNEBaseDTO<ElectricEnergyMeterDTO>(RestResponseCode.Created)
                {
                    Result = new ElectricEnergyMeterDTO()
                    {
                        Id = createEEMResult.Id,
                        Number = createEEMResult.Number,
                        EEMeterTypeId = createEEMResult.EEMeterTypeId,
                        VerificationDate = createEEMResult.VerificationDate,
                        VerificationPeriod = createEEMResult.VerificationPeriod,
                        EEMeterTypeDescription = eEMType.Description
                    }
                };
            }
        }
        /// <summary>
        /// Позволяет получить список всех счётчиков электрической энергии
        /// </summary>
        public async Task<TNEBaseDTO<ElectricEnergyMetersListDTO>> GetAllElectricEnergyMeters()
        {
            return new TNEBaseDTO<ElectricEnergyMetersListDTO>(RestResponseCode.OK)
            {
                Result = new ElectricEnergyMetersListDTO()
                {
                    ElectricEnergyMeters = (await electricEnergyMetersRepository.GetAll()).Select(x => new ElectricEnergyMetersListDTO.ElectricEnergyMeterListItemDTO()
                    {
                        Id = x.Id,
                        Number = x.Number,
                        VerificationDate = x.VerificationDate,
                        VerificationPeriod = x.VerificationPeriod,
                        EEMeterTypeId = x.EEMeterTypeId,
                        EEMeterTypeDescription = x.EEMeterType?.Description ?? "N/A"
                    }).ToList()
                }
            };
        }
    }
}
