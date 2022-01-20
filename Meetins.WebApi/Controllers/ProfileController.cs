using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.Ftp.Request;
using Meetins.BLL.DTOs.Profile.Request;
using Meetins.BLL.Interfaces;
using Meetins.BLL.Mapping;
using Meetins.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfileController : Controller
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
        public async Task<ActionResult<ProfileResponseModel>> MyProfile()
        {
            string rawUserId = HttpContext.User.FindFirstValue("userId");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            ProfileDto profileDto = await _profileService.GetUserProfile(userId);

            return Ok(profileDto.ToProfileResponseModel());
        }

        [Authorize]
        [HttpPost, Route("by-loginurl")]
        public async Task<ActionResult<ProfileResponseModel>> GetProfileByLoginUrl([FromBody] string loginUrl)
        {

            ProfileDto profileDto = await _profileService.GetUserProfileByLoginUrl(loginUrl);

            if (profileDto is null)
            {
                return BadRequest(new { errorText = "User with this login does not exist." });
            }

            return Ok(profileDto.ToProfileResponseModel());
        }

        [Authorize]
        [HttpPost, Route("update-status")]
        public async Task<ActionResult<ProfileResponseModel>> UpdateStatusAsync([FromBody] string newStatus)
        {
            string rawUserId = HttpContext.User.FindFirstValue("userId");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            ProfileDto profileDto = await _profileService.UpdateProfileStatus(new UpdateStatusRequestDto { UserId = userId, NewStatus = newStatus });

            return Ok(profileDto.ToProfileResponseModel());
        }

        [Authorize]
        [HttpPost, Route("update-avatar")]
        public async Task<ActionResult> UpdateAvatarAsync(IFormFile uploadedFile)
        {
            string rawUserId = HttpContext.User.FindFirstValue("userId");            

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            if (uploadedFile == null)
            {
                return BadRequest(new { errorText = "Uploaded file cannot be null." });
            }

            ProfileDto profile = await _profileService.GetUserProfile(userId);

            string newAvatarPath = await _ftpService.UploadNewAvatar(new UpdateAvatarRequestDto { OldAvatar = profile.UserIcon, UploadedFile = uploadedFile });

            ProfileDto profileDto = await _profileService.UpdateAvatarPath(new UpdateAvatarPathRequestDto { UserId = userId, NewAvatarPath = newAvatarPath });

            return Ok(profileDto.ToProfileResponseModel());
        }
    }
}

