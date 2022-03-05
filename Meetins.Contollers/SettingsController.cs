using Meetins.Abstractions.Services;
using Meetins.Communication.Abstractions;
using Meetins.Communication.Hubs;
using Meetins.Models.Mapper;
using Meetins.Models.Profile.Output;
using Meetins.Models.Settings.Input;
using Meetins.Models.User.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Meetins.Controllers
{    
    [ApiController, Route("settings")]
    public class SettingsController : ControllerBase
    {
        private IUserService _userService;
        private IProfileService _profileService;
        private readonly IHubContext<MessengerHub, IClients> _hubContext;
        public SettingsController(IUserService userService, IProfileService profileService, IHubContext<MessengerHub, IClients> hubContext)
        {
            _userService = userService;
            _profileService = profileService;
            _hubContext = hubContext;
        }

        [Authorize]
        [HttpGet, Route("check-login")]
        public async Task<ActionResult<UserOutput>> CheckLoginAsync(string login)
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
        public async Task<ActionResult<ProfileOutput>> UpdateProfileSettingsAsync([FromBody] UpdateProfileSettingsInput profileSettingsInput)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _userService.UpdateProfileSettingsAsync(userId, profileSettingsInput.Name, profileSettingsInput.PhoneNumber, profileSettingsInput.BirthDate);          

            return Ok(result.ToProfileOutput());
        }

        [Authorize]
        [HttpPost, Route("update-account")]
        public async Task<ActionResult<ProfileOutput>> UpdateAccountSettingsAsync([FromBody] UpdateAccountSettingsInput accountSettingsInput)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var loginCheck = await _userService.CheckUserByLoginAsync(accountSettingsInput.Login);

            if (loginCheck is not null)
            {
                return BadRequest(new { errortext = "Login already exists." });
            }

            var emailCheck = await _userService.CheckUserByEmailAsync(accountSettingsInput.Email);

            if (emailCheck is not null)
            {
                return BadRequest(new { errortext = "Email already exists." });
            }

            var result = await _userService.UpdateAccountSettingsAsync(userId, accountSettingsInput.Email, accountSettingsInput.Password, accountSettingsInput.Login);

            return Ok(result.ToProfileOutput());
        }
    }
}
