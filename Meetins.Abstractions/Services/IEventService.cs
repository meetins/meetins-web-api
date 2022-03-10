using Meetins.Models.Events.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция к сервису событий.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Метод вернёт список категорий событий.
        /// </summary>
        /// <returns>Список категорий событий.</returns>
        Task<IEnumerable<EventsCategoryOutput>> GetEventsCategotiesListAsync();
    }
}
