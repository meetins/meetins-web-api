using Meetins.Models.Events.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция к репозиторию событий.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Метод вернёт список категорий событий.
        /// </summary>
        /// <returns>Список категорий событий.</returns>
        Task<IEnumerable<EventsCategoryOutput>> GetEventsCategotiesListAsync();
    }
}
