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
    }
}
