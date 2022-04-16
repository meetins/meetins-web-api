using Meetins.Models.Events.Output;
using System;
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

        /// <summary>
        /// Метод вернёт список всех событий.
        /// </summary>
        /// <returns>Список всех событий.</returns>
        Task<IEnumerable<EventOutput>> GetEventsListAsync();

        /// <summary>
        /// Метод вернёт событие по Id.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        Task<EventOutput> GetEventByIdAsync(Guid userId, long eventId);

        /// <summary>
        /// Метод подпишет пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        Task<EventOutput> SubscribeToEventAsync(Guid userId, long eventId);

        /// <summary>
        /// Метод отменит подписку пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        Task<EventOutput> UnSubscribeToEventAsync(Guid userId, long eventId);
    }
}
