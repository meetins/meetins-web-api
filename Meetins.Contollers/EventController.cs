using Meetins.Abstractions.Services;
using Meetins.Models.Events.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
