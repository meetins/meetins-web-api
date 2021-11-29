using Meetins.BLL.DTO;
using Meetins.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Route("get-users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            return Ok(result);
        }

        [HttpPost, Route("get-token")]
        public async Task<ActionResult<string>> GetTokenAsync(string email, string password)
        {
            string jwt = await _userService.GenerateTokenAsync(email, password);

            if (jwt is null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            var response = new
            {
                access_token = jwt,
                username = "username"
            };

            return Json(response);
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            //db.Users.Add(user);
            //await db.SaveChangesAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpGet, Route("get-content")]
        public ActionResult<string> GetContent()
        {

            var content = new
            {
                content = "content"
            };
            return Json(content);
        }
    }
}
