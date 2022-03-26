using Meetins.Abstractions.Services;
using Meetins.Models.Events.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Contollers
{
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
        public async Task<IActionResult> GetEventsCategotiesListAsync()
        {
            var result = await _eventService.GetEventsCategotiesListAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод вернёт список всех событий.
        /// </summary>
        /// <returns>Список всех событий.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EventOutput>))]
        public async Task<IActionResult> GetEventsListAsync()
        {
            var result = await _eventService.GetEventsListAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод вернёт событие.
        /// </summary>
        /// <returns>Модель события.</returns>        
        [HttpPost]
        [Route("event-by-id")]
        [ProducesResponseType(200, Type = typeof(EventOutput))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetEventAsyncByIdAsync([FromBody] long eventId)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            //string rawUserId = "5db1031e-ca48-46d1-b9ea-d9e7ebb8c6e6";

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }            

            var result = await _eventService.GetEventByIdAsync(userId, eventId);

            return Ok(result);
        }

        /// <summary>
        /// Метод подпишет пользователя на событие.
        /// </summary>
        /// <returns>Модель события.</returns>        
        [HttpPost]
        [Route("subscribe")]
        [ProducesResponseType(200, Type = typeof(EventOutput))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> SubscribeToEventIdAsync([FromBody] long eventId)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            //string rawUserId = "5db1031e-ca48-46d1-b9ea-d9e7ebb8c6e6";

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _eventService.SubscribeToEventAsync(userId, eventId);

            return Ok(result);
        }

        /// <summary>
        /// Метод отменит подписку пользователя на событие.
        /// </summary>
        /// <returns>Модель события.</returns>        
        [HttpPost]
        [Route("unsubscribe")]
        [ProducesResponseType(200, Type = typeof(EventOutput))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UnSubscribeToEventIdAsync([FromBody] long eventId)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            //string rawUserId = "5db1031e-ca48-46d1-b9ea-d9e7ebb8c6e6";

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _eventService.UnSubscribeToEventAsync(userId, eventId);

            return Ok(result);
        }
    }
}
