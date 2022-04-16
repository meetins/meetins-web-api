﻿using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Entities;
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
        private PostgreDbContext _postgreDbContext;

        public EventRepository(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Метод вернёт событие по Id.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> GetEventByIdAsync(Guid userId, long eventId)
        {
            try
            {
                var result = await _postgreDbContext.Events.Where(e => e.EventId.Equals(eventId))
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
                List<Subscriber> subscribers = await _postgreDbContext.EventsToUsers
                    .Where(e => e.EventId == eventId && e.IsUserSubscribed == true)
                    .Include(e => e.User)
                    .Select(e => new Subscriber
                    {
                        UserId = e.UserId,
                        Name = e.User.Name,
                        Avatar = e.User.Avatar
                    }).ToListAsync();

                //подписан ли пользователь на событие
                var isUserSubscribed = await IsUserSubscribeToEventAsync(userId, eventId);

                result.IsInvitePossible = isUserSubscribed.IsUserSubscribed;
                result.IsSubscriptionPossible = !isUserSubscribed.IsUserSubscribed;
                result.Subscribers = subscribers;
                result.SubscribersCount = subscribers.Count;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
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
                var result = await _postgreDbContext.EventsCategories
                    .Select(e => new EventsCategoryOutput
                    {
                        EventsCategoryId = e.EventsCategotyId,
                        CategoryName = e.CategoryName
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
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
                var result = await _postgreDbContext.Events
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }


        /// <summary>
        /// Метод подпишет пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> SubscribeToEventAsync(Guid userId, long eventId)
        {
            try
            {
                var findedUser = await _postgreDbContext.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

                if (findedUser is null)
                {
                    throw new ArgumentException($"Пользователя с идентификатором {userId} не найдено.");
                }

                var findedEvent = await _postgreDbContext.Events.FirstOrDefaultAsync(u => u.EventId.Equals(eventId));

                if (findedEvent is null)
                {
                    throw new ArgumentException($"События с идентификатором {eventId} не найдено.");
                }

                var isUserSubscribe = await IsUserSubscribeToEventAsync(userId, eventId);

                if (isUserSubscribe is not null && isUserSubscribe.IsUserSubscribed)
                {
                    //пользователь уже подписан
                    return await GetEventByIdAsync(userId, eventId);
                }

                if (isUserSubscribe is null)
                {
                    //раньше не подписывался
                    await _postgreDbContext.EventsToUsers
                    .AddAsync(new EventsToUsersEntity
                    {
                        EventId = eventId,
                        UserId = userId,
                        IsUserSubscribed = true,
                        LastSubscriptionDate = DateTime.Now
                    });
                }
                else
                {
                    //раньше подписывался
                    isUserSubscribe.IsUserSubscribed = true;
                }

                await _postgreDbContext.SaveChangesAsync();

                //собираем событие
                var result = await _postgreDbContext.Events.Where(e => e.EventId.Equals(eventId))
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

                //список подписчиков
                List<Subscriber> subscribers = await _postgreDbContext.EventsToUsers
                    .Where(e => e.EventId == eventId && e.IsUserSubscribed == true)
                    .Include(e => e.User)
                    .Select(e => new Subscriber
                    {
                        UserId = e.UserId,
                        Name = e.User.Name,
                        Avatar = e.User.Avatar
                    }).ToListAsync();

                result.IsInvitePossible = true;
                result.IsSubscriptionPossible = false;
                result.Subscribers = subscribers;
                result.SubscribersCount = subscribers.Count;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод отменит подписку пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        public async Task<EventOutput> UnSubscribeToEventAsync(Guid userId, long eventId)
        {
            try
            {
                var findedUser = await _postgreDbContext.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

                if (findedUser is null)
                {
                    throw new ArgumentException($"Пользователя с идентификатором {userId} не найдено.");
                }

                var findedEvent = await _postgreDbContext.Events.FirstOrDefaultAsync(u => u.EventId.Equals(eventId));

                if (findedEvent is null)
                {
                    throw new ArgumentException($"События с идентификатором {eventId} не найдено.");
                }

                var isUserSubscribe = await IsUserSubscribeToEventAsync(userId, eventId);

                if (isUserSubscribe is null || !isUserSubscribe.IsUserSubscribed)
                {
                    //уже отписан 
                    return await GetEventByIdAsync(userId, eventId);
                }

                // подписан
                isUserSubscribe.IsUserSubscribed = false;                

                await _postgreDbContext.SaveChangesAsync();

                //собираем событие
                var result = await _postgreDbContext.Events.Where(e => e.EventId.Equals(eventId))
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

                //список подписчиков
                List<Subscriber> subscribers = await _postgreDbContext.EventsToUsers
                    .Where(e => e.EventId == eventId && e.IsUserSubscribed == true)
                    .Include(e => e.User)
                    .Select(e => new Subscriber
                    {
                        UserId = e.UserId,
                        Name = e.User.Name,
                        Avatar = e.User.Avatar
                    }).ToListAsync();

                result.IsInvitePossible = false;
                //TODO: сделать возможность подписаться по таймауту
                result.IsSubscriptionPossible = true;
                result.Subscribers = subscribers;
                result.SubscribersCount = subscribers.Count;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }


        /// <summary>
        /// Метод возвращает состояние подписки пользователя на событие.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>true - если пользователь подписан, false - если не подписан.</returns>
        public async Task<EventsToUsersEntity> IsUserSubscribeToEventAsync(Guid userId, long eventId)
        {
            try
            {
                var result = await _postgreDbContext.EventsToUsers
                    .FirstOrDefaultAsync(e => e.EventId.Equals(eventId) && e.UserId.Equals(userId));

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }
    }
}
