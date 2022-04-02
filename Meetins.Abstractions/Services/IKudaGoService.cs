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

        /// <summary>
        /// Получение списка всех доступных мест.
        /// </summary>
        /// <returns> Список всех доступных мест. </returns>
        Task<IEnumerable<Results>> GetAllAvailablePlacesAsync();
    }
}
