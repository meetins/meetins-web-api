using Meetins.Models.KudaGo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция сервиса доступных городов.
    /// </summary>
    public interface IKudaGoService
    {
        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список всех доступных городов. </returns>
        Task<IEnumerable<KudaGoOutput>> GetAllAvailableCitiesAsync();
    }
}
