using Meetins.Models.Common;
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
        /// Метод генерирует 6-значный цифробуквенный код.
        /// </summary>
        /// <returns>6-значный цифробуквенный код.</returns>
        Task<string> GenerateCodeAsync();
    }
}
