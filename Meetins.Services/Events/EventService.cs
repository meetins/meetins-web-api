using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Models.Events.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.Events
{
    /// <summary>
    /// Сервис событий.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Метод вернёт событие по Id.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> GetEventByIdAsync(Guid eventId)
        {
            try
            {
                var result = await _eventRepository.GetEventByIdAsync(eventId);

                return result;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод вернёт список категорий событий.
        /// </summary>
        /// <returns>Список категорий событий.</returns>
        public async Task<IEnumerable<EventsCategoryOutput>> GetEventsCategotiesListAsync()
        {
            try
            {
                var result = await _eventRepository.GetEventsCategotiesListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод вернёт список всех событий.
        /// </summary>
        /// <returns>Список всех событий.</returns>
        public async Task<IEnumerable<EventOutput>> GetEventsListAsync()
        {
            try
            {
                var result = await _eventRepository.GetEventsListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод подпишет пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> SubscribeToEventAsync(Guid userId, Guid eventId)
        {
            try
            {
                var result = await _eventRepository.SubscribeToEventAsync(userId, eventId);

                return result;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод отменит подписку пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> UnSubscribeToEventAsync(Guid userId, Guid eventId)
        {
            try
            {
                var result = await _eventRepository.UnSubscribeToEventAsync(userId, eventId);

                return result;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }
    }
}
