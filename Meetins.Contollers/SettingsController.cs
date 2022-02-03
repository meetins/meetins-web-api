using Meetins.Abstractions.Services;
using Meetins.Models.Mapper;
using Meetins.Models.Profile.Output;
using Meetins.Models.User.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

//test commit 2
namespace Meetins.Controllers
{    
    [ApiController, Route("settings")]
    public class SettingsController : ControllerBase
    {
        private IUserService _userService;
        private IProfileService _profileService;
        public SettingsController(IUserService userService, IProfileService profileService)
        {
            _userService = userService;
            _profileService = profileService;
        }

        [Authorize]
        [HttpGet, Route("check-login")]
        public async Task<ActionResult<UserOutput>> CheckLoginurl(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return BadRequest(new { errorText = "LoginUrl cannot be null or empty." });
            }

            var user = await _userService.CheckUserByLoginAsync(login);

            return Ok(user);

        }

        [Authorize]
        [HttpPost, Route("update-profile")]
        public async Task<ActionResult<ProfileOutput>> EditProfileSettings([FromBody] string name, string phone, string birthDate)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").ToString();

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _userService.UpdateProfileSettingsAsync(userId, name, phone, birthDate);
                       
            return Ok(result.ToProfileOutput());
        }

        [Authorize]
        [HttpPost, Route("update-account")]
        public async Task<ActionResult<ProfileOutput>> EditAccountSettings([FromBody] string email, string password, string login)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").ToString();

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var loginCheck = await _userService.CheckUserByLoginAsync(login);

            if (loginCheck is not null)
            {
                return BadRequest(new { errortext = "Login already exists." });
            }

            var emailCheck = await _userService.CheckUserByEmailAsync(email);

            if (emailCheck is not null)
            {
                return BadRequest(new { errortext = "Email already exists." });
            }

            var result = await _userService.UpdateAccountSettingsAsync(userId, email, password, login);

            return Ok(result.ToProfileOutput());
        }
    }
}
