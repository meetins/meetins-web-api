using Meetins.BLL.DTOs.AccountSettings.Requests;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models.AccountSettings.Requests;
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
    }
}
