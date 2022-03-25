using Meetins.Models.KudaGo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция репозитория доступных городов.
    /// </summary>
    public interface IKudaGoRepository
    {
        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список всех доступных городов. </returns>
        Task<IEnumerable<KudaGoOutput>> GetAllAvailableCitiesAsync();
    }
}
