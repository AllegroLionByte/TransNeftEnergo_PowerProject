using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TNEPowerProject.Contract.DTO.Transformers;

namespace TNEPowerProject.Contract.Interfaces
{
    /// <summary>
    /// Представляет описание API для типов трансформаторов
    /// </summary>
    public interface ITransformerTypesAPI
    {
        ///// <summary>
        ///// Метод для создания нового типа трансформатора
        ///// </summary>
        ///// <param name="transformerTypeDTO"></param>
        ///// <returns></returns>
        //[HttpPost]
        //Task<TransformerTypeDTO> CreateTransformerType(TransformerTypeDTO transformerTypeDTO);
        /// <summary>
        /// Получает список всех типов трансформаторов
        /// </summary>
        [HttpGet("list")]
        Task<TransformerTypesListDTO> GetAll();
    }
}
