using Meetins.Abstractions.Services;
using Meetins.Models.KudaGo;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(await _kudaGoService.GetAllAvailableCitiesAsync());
        }
    }
}
