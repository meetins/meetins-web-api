using Meetins.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("get-token")]
        public async Task<ActionResult<string>> GetTokenAsync(string email, string password)
        {
            string jwt = await _authService.GenerateTokenAsync(email, password);

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
