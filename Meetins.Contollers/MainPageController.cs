using Meetins.Abstractions.Services;
using Meetins.Models.MainPage.Output;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    /// <summary>
    /// Контроллер для главной страницы приложения.
    /// </summary>
    [Route("main-page")]
    [ApiController]
    public class MainPageController : ControllerBase
    {
        IAboutService _aboutService;

        public MainPageController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        [Route("get-about")]
        public async Task<ActionResult<IEnumerable<AboutsOutput>>> GetAboutsAsync()
        {
            var result = await _aboutService.GetAboutsAsync();

            return Ok(result);
        }
    }
}
