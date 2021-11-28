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
        IHeaderService _headerService;

        public MainPageController(IHeaderService headerService)
        {
            _headerService = headerService;
        }

        [HttpGet, Route("get-headers")]
        public async Task<ActionResult<IEnumerable<HeaderDto>>> GetHeaders()
        {
            var result = await _headerService.GetAllHeadersAsync();

            return Ok(result);
        }
    }
}
