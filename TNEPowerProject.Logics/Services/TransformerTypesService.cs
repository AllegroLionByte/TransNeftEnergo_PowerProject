using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TNEPowerProject.Domain.Entities;
using TNEPowerProject.Contract.Enums;
using TNEPowerProject.Infrastructure.Repository;
using TNEPowerProject.Logics.Interfaces.Services;
using TNEPowerProject.Contract.DTO.Transformers;
using TNEPowerProject.Infrastructure.Database.EFCore;
using System;

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
        public TransformerTypesService(EnergoDBContext dbContext, ILogger logger)
        {
            this.transformerTypesRepository = new TransformerTypesRepository(dbContext, logger);
        }
        /// <summary>
        /// Позволяет добавить новый тип трансформатора
        /// </summary>
        /// <param name="transformerTypeDTO">Сущность, описывающая новый тип трансформатора</param>
        public async Task<TransformerTypeDTO> CreateTransformerType(TransformerTypeDTO transformerTypeDTO)
        {
            if (string.IsNullOrWhiteSpace(transformerTypeDTO.Description) || !Enum.IsDefined(typeof(TransformerType.TransformerPurpose), transformerTypeDTO.TransformerPurpose))
            {
                transformerTypeDTO.StatusCode = (int)RestResponseCode.BadRequest;
                return transformerTypeDTO;
            }
            await transformerTypesRepository.Add(new TransformerType()
            {
                Description = transformerTypeDTO.Description,
                Purpose = (TransformerType.TransformerPurpose)transformerTypeDTO.TransformerPurpose
            });
            transformerTypeDTO.StatusCode = (int)RestResponseCode.Created;
            return transformerTypeDTO;
        }
        /// <summary>
        /// Позволяет получить список всех типов трансформаторов
        /// </summary>
        public async Task<TransformerTypesListDTO> GetAllTransformerTypes()
        {
            return new TransformerTypesListDTO(RestResponseCode.OK)
            {
                TransformerTypes = (await transformerTypesRepository.GetAll()).Select(x => new TransformerTypesListDTO.TransformerTypeListItemDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    TransformerPurpose = (int)x.Purpose
                }).ToList()
            };
        }
    }
}
