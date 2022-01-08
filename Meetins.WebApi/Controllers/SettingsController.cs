using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.Settings.Requests;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models.Responses;
using Meetins.WebApi.Models.Settings.Requests;
using Meetins.WebApi.Models.Settings.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//test commit
namespace Meetins.WebApi.Controllers
{
    [Route("settings")]
    [ApiController]
    public class SettingsController : Controller
    {
        private IUserService _userService;
        private IProfileService _profileService;
        public SettingsController(IUserService userService, IProfileService profileService)
        {
            _userService = userService;
            _profileService = profileService;
        }

        [Authorize]
        [HttpGet, Route("check-login-url")]
        public async Task<ActionResult<CheckLoginUrlResponseModel>> CheckLoginurl(string loginUrl)
        {
            if (string.IsNullOrEmpty(loginUrl))
            {
                return BadRequest(new { errorText = "LoginUrl cannot be null or empty." });
            }

            UserDto user = await _userService.CheckUserByLoginUrl(loginUrl);

            CheckLoginUrlResponseModel checkLoginUrlResponse = new CheckLoginUrlResponseModel()
            {
                LoginUrl = loginUrl
            };

            checkLoginUrlResponse.IsExists = user is not null;

            return Ok(checkLoginUrlResponse);

        }

        [Authorize]
        [HttpPost, Route("edit-profile")]
        public async Task<ActionResult<ProfileResponseModel>> EditProfileSettings([FromBody] EditProfileSettingsRequsetModel editProfileSettingsRequest)
        {
            string rawUserId = HttpContext.User.FindFirstValue("userId");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            UserDto userDto = await _userService.CheckUserByPhone(editProfileSettingsRequest.PhoneNumber);

            if (userDto is not null)
            {
                return BadRequest(new { errortext = "PhoneNumber already exists." });
            }

            try
            {
                string[] names = editProfileSettingsRequest.FirstNameAndLastName.Split(' ');
            
                EditProfileSettingsRequestDto editAProfileSettingsRequestDto = new EditProfileSettingsRequestDto()
                {
                    UserId = userId,
                    FirstName = names[0],
                    LastName = names[1],
                    PhoneNumber = editProfileSettingsRequest.PhoneNumber
                };

                try
                {
                    editAProfileSettingsRequestDto.BirthDate = DateTime.Parse(editProfileSettingsRequest.BirthDate);
                }
                catch (Exception)
                {
                    editAProfileSettingsRequestDto.BirthDate = null;
                }
                await _userService.EditProfileSettings(editAProfileSettingsRequestDto);

                             
            }       
            catch(Exception)
            {
                return BadRequest();
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
                DateRegister = profileDto.DateRegister,
                BirthDate = profileDto.BirthDate,
                LoginUrl = profileDto.LoginUrl
            };
            return Ok(profile);

        }

        [Authorize]
        [HttpPost, Route("edit-account")]
        public async Task<ActionResult<ProfileResponseModel>> EditAccountSettings([FromBody] EditAccountSettingsRequestModel editAccountSettingsRequest)
        {
            string rawUserId = HttpContext.User.FindFirstValue("userId");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            UserDto userDto = await _userService.CheckUserByLoginUrl(editAccountSettingsRequest.LoginUrl);

            if (userDto is not null)
            {
                return BadRequest(new { errortext = "LoginUrl already exists." });
            }

            UserDto userDto2 = await _userService.CheckUserByEmail(editAccountSettingsRequest.Email);

            if (userDto2 is not null)
            {
                return BadRequest(new { errortext = "Email already exists." });
            }

            EditAccountSettingsRequestDto editAccountSettingsRequestDto = new EditAccountSettingsRequestDto()
            {
                UserId = userId,               
                Email = editAccountSettingsRequest.Email,                
                Password = editAccountSettingsRequest.Password,               
                LoginUrl = editAccountSettingsRequest.LoginUrl
            };

            await _userService.EditAccountSettings(editAccountSettingsRequestDto);

            ProfileDto profileDto = await _profileService.GetUserProfile(userId);

            ProfileResponseModel profile = new ProfileResponseModel
            {
                FirstName = profileDto.FirstName,
                LastName = profileDto.LastName,
                Email = profileDto.Email,
                PhoneNumber = profileDto.PhoneNumber,
                Gender = profileDto.Gender,
                UserIcon = profileDto.UserIcon,
                DateRegister = profileDto.DateRegister,
                BirthDate = profileDto.BirthDate,
                LoginUrl = profileDto.LoginUrl
            };

            return Ok(profile);
        }
    }
}
