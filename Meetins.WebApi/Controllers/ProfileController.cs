using Meetins.BLL.DTOs;
using Meetins.BLL.Interfaces;
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

            ProfileResponseModel profile = new ProfileResponseModel
            {
                FirstName = profileDto.FirstName,
                LastName = profileDto.LastName,
                Email = profileDto.Email,
                PhoneNumber = profileDto.PhoneNumber,
                Gender = profileDto.Gender,
                UserIcon = profileDto.UserIcon,
                DateRegister = profileDto.DateRegister
            };

            return Ok(profile);
        }
    }
}
