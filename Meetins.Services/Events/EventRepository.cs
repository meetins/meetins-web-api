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
        /// Метод вернёт событие по Id.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> GetEventByIdAsync(Guid userId, Guid eventId)
        {
            try
            {
                var result = await _context.Events.Where(e => e.EventId.Equals(eventId))
                    .Include(e => e.EventsCategory)
                    .Select(e => new EventOutput
                    {
                        EventId = e.EventId,
                        CategoryName = e.EventsCategory.CategoryName,
                        Title = e.Title,
                        Description = e.Description,
                        MainPoster = e.MainPoster
                    })
                    .FirstOrDefaultAsync();


                if (result is null)
                {
                    throw new ArgumentException($"События с идентификатором {eventId} не найдено.");
                }

                //список подписчиков
                List<Subscriber> subscribers = await _context.EventsToUsers
                    .Where(e => e.EventId == eventId && e.IsUserSubscribed == true)
                    .Include(e => e.User)
                    .Select(e => new Subscriber
                    {
                        UserId = e.UserId,
                        Name = e.User.Name,
                        Avatar = e.User.Avatar
                    }).ToListAsync();

                //подписан ли пользователь на событие
                bool isUserSubscribed = await IsUserSubscribeToEventAsync(userId, eventId);

                result.IsInvitePosiible = isUserSubscribed;
                result.IsSubscriptionPossible = !isUserSubscribed;
                result.Subscribers = subscribers;
                result.SubscribersCount = subscribers.Count;

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

        /// <summary>
        /// Метод вернёт список всех событий.
        /// </summary>
        /// <returns>Список всех событий.</returns>
        public async Task<IEnumerable<EventOutput>> GetEventsListAsync()
        {
            try
            {
                var result = await _context.Events
                       .Include(e => e.EventsCategory)
                       .Select(e => new EventOutput
                       {
                           EventId = e.EventId,
                           CategoryName = e.EventsCategory.CategoryName,
                           Title = e.Title,
                           Description = e.Description,
                           MainPoster = e.MainPoster
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


        /// <summary>
        /// Метод подпишет пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> SubscribeToEventAsync(Guid userId, Guid eventId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод отменит подписку пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> UnSubscribeToEventAsync(Guid userId, Guid eventId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод возвращает состояние подписки пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>true - если пользователь подписан, false - если не подписан.</returns>
        public async Task<bool> IsUserSubscribeToEventAsync(Guid userId, Guid eventId)
        {
            try
            {
                var result = await _context.EventsToUsers
                    .Where(e => e.EventId.Equals(eventId) && e.UserId.Equals(userId))
                    .FirstOrDefaultAsync();

                if (result is null)
                {
                    return false;
                }

                return result.IsUserSubscribed;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }
    }
}
