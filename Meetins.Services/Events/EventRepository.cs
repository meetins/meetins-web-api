using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.Events.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.Events
{
    /// <summary>
    /// Репозиторий событий.
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private PostgreDbContext _context;

        public EventRepository(PostgreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод вернёт список категорий событий.
        /// </summary>
        /// <returns>Список категорий событий.</returns>
        public async Task<IEnumerable<EventsCategoryOutput>> GetEventsCategotiesListAsync()
        {
            try
            {
                var result = await _context.EventsCategories
                    .Select(e => new EventsCategoryOutput
                    {
                        EventsCategoryId = e.EventsCategotyId,
                        CategoryName = e.CategoryName
                    })
                    .ToListAsync();

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
