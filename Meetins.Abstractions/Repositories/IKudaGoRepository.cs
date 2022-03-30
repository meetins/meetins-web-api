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
        Task<IEnumerable<KudaGoCitiesOutput>> GetAllAvailableCitiesAsync();

        /// <summary>
        /// Получение списка всех категорий событий.
        /// </summary>
        /// <returns> Список всех категорий событий. </returns>
        Task<IEnumerable<KudaGoCategoriesOutput>> GetAllEventСategoriesAsync();

        /// <summary>
        /// Получение списка всех категорий мест.
        /// </summary>
        /// <returns> Список категорий мест. </returns>
        Task<IEnumerable<KudaGoCategoriesOutput>> GetAllPlaceСategoriesAsync();
    }
}
