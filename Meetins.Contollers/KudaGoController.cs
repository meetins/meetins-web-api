using Meetins.Abstractions.Services;
using Meetins.Models.Exceptions;
using Meetins.Models.KudaGo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Contollers
{
    /// <summary>
    /// В контроллере содержится функционал для получения информации из сервиса KudaGo.
    /// </summary>
    [Route("kudago")]
    [ApiController]
    public class KudaGoController : ControllerBase
    {
        private IKudaGoService _kudaGoService;

        public KudaGoController(IKudaGoService kudaGoService)
        {
            _kudaGoService = kudaGoService;
        }

        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список всех доступных городов. </returns>
        [HttpGet]
        [Route("cities")]
        public async Task<ActionResult<IEnumerable<KudaGoCitiesOutput>>> GetAllAvailableCitiesAsync()
        {
            try
            {
                return Ok(await _kudaGoService.GetAllAvailableCitiesAsync());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Получение списка всех категорий событий.
        /// </summary>
        /// <returns> Список категорий событий. </returns>
        [HttpGet]
        [Route("event-categories")]
        public async Task<ActionResult<IEnumerable<KudaGoCategoriesOutput>>> GetAllEventСategoriesAsync()
        {
            try
            {
                return Ok(await _kudaGoService.GetAllEventСategoriesAsync());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new { messge = e.Message });
            }
        }

        /// <summary>
        /// Получение списка всех категорий мест.
        /// </summary>
        /// <returns> Список категорий мест. </returns>
        [HttpGet]
        [Route("place-categories")]
        public async Task<ActionResult<IEnumerable<KudaGoCategoriesOutput>>> GetAllPlaceСategoriesAsync()
        {
            try
            {
                return Ok(await _kudaGoService.GetAllPlaceСategoriesAsync());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new { messge = e.Message });
            }
        }

        /// <summary>
        /// Получение списка всех доступных мест.
        /// </summary>
        /// <returns> Список всех доступных мест. </returns>
        [HttpGet]
        [Route("places")]
        public async Task<ActionResult<KudaGoPlacesOutput>> GetAllAvailablePlacesAsync(int numberOfPage = 1)
        {
            try
            {
                return Ok(await _kudaGoService.GetAllAvailablePlacesAsync(numberOfPage));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new { messge = e.Message });
            }
        }
    }
}
