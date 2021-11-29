using Meetins.BLL.DTO;
using Meetins.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("main-page")]
    [ApiController]
    public class MainPageController : Controller
    {
        IAboutService _aboutService;

        public MainPageController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet, Route("get-about")]
        public async Task<ActionResult<IEnumerable<AboutDto>>> GetAboutsAsync()
        {
            var result = await _aboutService.GetAboutsAsync();

            return Ok(result);
        }
    }
}
