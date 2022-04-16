using System.Collections.Generic;
using System.Threading.Tasks;
using Meetins.Abstractions.Services;
using Meetins.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace Meetins.Contollers
{
    /// <summary>
    /// В контроллере содержится общий функционал.
    /// </summary>
    [Route("common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        /// <summary>
        /// Получение списка всех городов пользователей.
        /// </summary>
        /// <returns> Список всех городов пользователей. </returns>
        [HttpGet]
        [Route("cities")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CityOutput>))]
        public async Task<ActionResult<IEnumerable<CityOutput>>> GetAllCitiesAsync()
        {
            var result = await _commonService.GetAllCitiesAsync();

            return Ok(result);
        }
    }
}
