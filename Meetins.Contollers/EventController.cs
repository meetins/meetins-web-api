using Meetins.Abstractions.Services;
using Meetins.Models.Events.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Contollers
{
    /// <summary>
    /// В контроллере содержатся методы для работы с событиями.
    /// </summary>
    [Route("events")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Метод вернёт список категорий событий.
        /// </summary>
        /// <returns>Список категорий событий.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventsCategoryOutput>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEventsCategotiesListAsync()
        {
            try
            {
                var result = await _eventService.GetEventsCategotiesListAsync();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });    
                //TODO: log
            }            
        }

        /// <summary>
        /// Метод вернёт список всех событий.
        /// </summary>
        /// <returns>Список всех событий.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventOutput>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEventsListAsync()
        {
            try
            {
                var result = await _eventService.GetEventsListAsync();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
                //TODO: log
            }
        }


        /// <summary>
        /// Метод вернёт событие по идентификатору.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>
        [HttpPost]
        [Route("event-by-id")]
        [ProducesResponseType(200, Type = typeof(EventOutput))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetEventAsyncByIdAsync([FromBody] long eventId)
        {
            try
            {
                string rawUserId = HttpContext.User.FindFirst("userId").Value;

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var result = await _eventService.GetEventByIdAsync(userId, eventId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
                //TODO: log
            }
        }

        /// <summary>
        /// Метод подпишет пользователя на событие.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>        
        [HttpPost]
        [Route("subscribe")]
        [ProducesResponseType(200, Type = typeof(EventOutput))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> SubscribeToEventIdAsync([FromBody] long eventId)
        {
            try
            {
                string rawUserId = HttpContext.User.FindFirst("userId").Value;

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var result = await _eventService.SubscribeToEventAsync(userId, eventId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
                //TODO: log
            }
        }

        /// <summary>
        /// Метод отменит подписку пользователя на событие.
        /// </summary>
        /// <param name="eventId">Идентификатор события.</param>
        /// <returns>Модель события.</returns>        
        [HttpPost]
        [Route("unsubscribe")]
        [ProducesResponseType(200, Type = typeof(EventOutput))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UnSubscribeToEventIdAsync([FromBody] long eventId)
        {
            try
            {
                string rawUserId = HttpContext.User.FindFirst("userId").Value;

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var result = await _eventService.UnSubscribeToEventAsync(userId, eventId);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
                //TODO: log
            }
        }
    }
}
