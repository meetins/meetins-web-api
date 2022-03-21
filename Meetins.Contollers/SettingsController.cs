using Meetins.Abstractions.Services;
using Meetins.Models.Mapper;
using Meetins.Models.Profile.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    /// <summary>
    /// Класс контроллера для работы с настройками.
    /// </summary>
    [ApiController]
    [Route("settings")]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private IUserService _userService;       
        private IDialogsService _dialogsService;        
        public SettingsController(IUserService userService, IDialogsService dialogsService)
        {
            _userService = userService;            
            _dialogsService = dialogsService;
        }

        /// <summary>
        /// Метод вернет пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Профиль пользователя.</returns>
        [HttpGet]
        [Route("check-login")]
        public async Task<ActionResult<ProfileOutput>> CheckLoginAsync(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return BadRequest(new { errorText = "Login cannot be null or empty." });
            }

            var user = await _userService.GetUserByLoginAsync(login);

            return Ok(user.ToProfileOutput());
        }       

        /// <summary>
        /// Полное удаление аккаунта пользователя.
        /// </summary>
        /// <returns></returns>        
        [HttpDelete]
        [Route("delete-user")]
        public async Task<ActionResult<bool>> DeleteAsync()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            //string rawUserId = "39b8dd6f-37b1-4235-8f51-9dcb313f46d3";

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var deleteDialogStatus = await _dialogsService.DeleteAllUserDialogsAndMessagesAsync(userId);
            var deleteUserStatus = await _userService.DeleteUserByUserIdAsync(userId);
            var deleteTokenStatus = await _userService.DeleteAllRefreshTokensByUserIdAsync(userId);

            return Ok(deleteDialogStatus && deleteUserStatus && deleteTokenStatus);
        }
    }
}
