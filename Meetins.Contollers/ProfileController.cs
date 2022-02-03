using Meetins.Abstractions.Services;
using Meetins.Models.Profile.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IProfileService _profileService;
        private IFtpService _ftpService;
        public ProfileController(IProfileService profileService, IFtpService ftpService)
        {
            _profileService = profileService;
            _ftpService = ftpService;
        }

        [Authorize]
        [HttpGet, Route("my-profile")]
        public async Task<ActionResult<ProfileOutput>> GetUserProfileAsync()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").ToString();

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var profile = await _profileService.GetUserProfileAsync(userId);

            return Ok(profile);
        }

        [Authorize]
        [HttpPost, Route("by-login")]
        public async Task<ActionResult<ProfileOutput>> GetProfileByLoginUrl([FromBody] string login)
        {

            var profile = await _profileService.GetUserProfileByLoginAsync(login);

            if (profile is null)
            {
                return BadRequest(new { errorText = "User with this login does not exist." });
            }

            return Ok(profile);
        }

        [Authorize]
        [HttpPost, Route("update-status")]
        public async Task<ActionResult<ProfileOutput>> UpdateStatusAsync([FromBody] string newStatus)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").ToString();

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            ProfileOutput profile = await _profileService.UpdateProfileStatusAsync(userId, newStatus);

            return Ok(profile);
        }

        [Authorize]
        [HttpPost, Route("update-avatar")]
        public async Task<ActionResult<ProfileOutput>> UpdateAvatarAsync(IFormFile uploadedFile)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").ToString();

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            if (uploadedFile == null)
            {
                return BadRequest(new { errorText = "Uploaded file cannot be null." });
            }

            ProfileOutput profile = await _profileService.GetUserProfileAsync(userId);

            string newAvatarPath = await _ftpService.UpdateAvatar(profile.Avatar, uploadedFile);

            ProfileOutput res = await _profileService.UpdateAvatarPathAsync(userId, newAvatarPath);

            return Ok(res);
        }
    }
}

