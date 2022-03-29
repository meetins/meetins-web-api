using Meetins.Abstractions.Services;
using Meetins.Core.Exceptions;
using Meetins.Models.KudaGo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
        public async Task<ActionResult<IEnumerable<KudaGoOutput>>> GetAllAvailableCitiesAsync()
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
    }
}
