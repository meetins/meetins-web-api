using Meetins.BLL.DTOs;
using Meetins.BLL.Interfaces;
using Meetins.BLL.Mapping;
using Meetins.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
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
    }
}
