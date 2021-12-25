using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.AccountSettings.Requests;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models.AccountSettings.Requests;
using Meetins.WebApi.Models.AccountSettings.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("account-settings")]
    [ApiController]
    public class AccountSettingsController : Controller
    {
        private IUserService _userService;
        public AccountSettingsController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost, Route("edit")]
        public async Task<IActionResult> EditAccountSettings([FromBody] EditAccountSettingsRequestModel editAccountSettingsRequest)
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

            string[] names = editAccountSettingsRequest.FisrtNameAndLastName.Split(' ');

            EditAccountSettingsRequestDto editAccountSettingsRequestDto = new EditAccountSettingsRequestDto()
            {
                UserId = userId,
                FirstName = names[0],
                LastName = names[1],
                Email = editAccountSettingsRequest.Email,
                PhoneNumber = editAccountSettingsRequest.PhoneNumber,
                Password = editAccountSettingsRequest.Password,
                BirthDate = DateTime.Parse(editAccountSettingsRequest.BirthDate),
                LoginUrl = editAccountSettingsRequest.LoginUrl
            };

            await _userService.EditAccountSettings(editAccountSettingsRequestDto);

            return Ok();
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
    }
}
