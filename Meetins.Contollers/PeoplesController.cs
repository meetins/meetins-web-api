using Meetins.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    [Route("peoples")]
    [ApiController]
    public class PeoplesController : ControllerBase
    {
        private IUserService _userService;
        
        public PeoplesController(IUserService userService)
        {
            _userService = userService;            
        }

        
        [HttpGet, Route("get-all")]
        public async Task<ActionResult<string>> GetAllPeoplesAsync()
        {            
            return Ok("qq");
        }
    }
}
