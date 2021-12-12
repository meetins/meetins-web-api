using Meetins.BLL.DTO;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models.Responses;
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
        public async Task<ActionResult<IEnumerable<AboutsModelView>>> GetAboutsAsync()
        {
            IEnumerable<AboutDto> result = await _aboutService.GetAboutsAsync();

            List<AboutsModelView> abouts = new List<AboutsModelView>();


            foreach (var item in result)
            {
                abouts.Add(new AboutsModelView
                    {
                        MainText = item.MainText,
                        Description = item.Description
                    });
            }

            return Ok(abouts);
        }
    }
}
