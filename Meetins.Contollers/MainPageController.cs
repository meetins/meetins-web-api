using Meetins.Abstractions.Services;
using Meetins.Communication.Abstractions;
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
        IMailingService _mailingService;

        public MainPageController(IAboutService aboutService, IMailingService mailingService)
        {
            _aboutService = aboutService;
            _mailingService = mailingService;
        }

        [HttpGet]
        [Route("get-about")]
        public async Task<ActionResult<IEnumerable<AboutsOutput>>> GetAboutsAsync()
        {
            var result = await _aboutService.GetAboutsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("send-code")]
        public async Task<ActionResult<IEnumerable<AboutsOutput>>> SendCodeAsync()
        {
            await _mailingService.SendAcceptCodeMailAsync("123456", "dmchdmka@gmail.com");

            return Ok();
        }
    }
}
