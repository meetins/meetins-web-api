using Meetins.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракия для общего сервиса.
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// Получение списка всех городов пользователей.
        /// </summary>
        /// <returns> Список всех городов пользователей. </returns>
        Task<IEnumerable<CityOutput>> GetAllCitiesAsync();

        /// <summary>
        /// Получение название города по идентификатору города
        /// </summary>
        /// <param name="cityId">Идентификатор города</param>
        /// <returns>Название города</returns>
        Task<string> GetCityNameAsync(Guid cityId);

        /// <summary>
        /// Метод генерирует 6-значный цифробуквенный код.
        /// </summary>
        /// <returns>6-значный цифробуквенный код.</returns>
        Task<string> GenerateCodeAsync();
    }
}
