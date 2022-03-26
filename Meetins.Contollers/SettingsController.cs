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
        [AllowAnonymous]
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
        /// Метод обновит логин пользователя.
        /// </summary>
        /// <param name="login">Новый логин.</param>
        /// <returns>Данные профиля.</returns>
        [HttpPost]
        [Route("update-login")]
        public async Task<ActionResult<ProfileOutput>> UpdateLoginAsync(string login)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _userService.UpdateLoginAsync(userId, login);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });               
            }            
        }

        /// <summary>
        /// Метод обновит почту пользователя.
        /// </summary>
        /// <param name="email">Новая почта.</param>
        /// <returns>Данные профиля.</returns>
        [HttpPost]
        [Route("update-email")]
        public async Task<ActionResult<ProfileOutput>> UpdateEmailAsync(string email)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _userService.UpdateEmailAsync(userId, email);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Метод обновит телефон пользователя.
        /// </summary>
        /// <param name="phone">Новый телефон.</param>
        /// <returns>Данные профиля.</returns>
        [HttpPost]
        [Route("update-phone")]
        public async Task<ActionResult<ProfileOutput>> UpdatePhoneAsync(string phone)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _userService.UpdatePhoneNumberAsync(userId, phone);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Метод обновит имя пользователя.
        /// </summary>
        /// <param name="name">Новое имя.</param>
        /// <returns>Данные профиля.</returns>
        [HttpPost]
        [Route("update-name")]
        public async Task<ActionResult<ProfileOutput>> UpdateNameAsync(string name)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _userService.UpdateNameAsync(userId, name);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Метод обновит пароль пользователя.
        /// </summary>
        /// <param name="password">Новый пароль.</param>
        /// <returns>Данные профиля.</returns>
        [HttpPost]
        [Route("update-password")]
        public async Task<ActionResult<ProfileOutput>> UpdatePasswordAsync(string password)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _userService.UpdatePasswordAsync(userId, password);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Метод обновит дату рождения пользователя.
        /// </summary>
        /// <param name="birthDate">Новая дата рождения.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-birthdate")]
        public async Task<ActionResult<ProfileOutput>> UpdateBirthDateAsync(string birthDate)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            if (DateTime.TryParse(birthDate, out DateTime date))
            {
                return BadRequest(new { message = "Ошибка. Передавайте дату в формате гггг-мм-дд." });
            }

            try
            {
                var result = await _userService.UpdateBirthDateAsync(userId, date);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Метод обновит город пользователя
        /// </summary>
        /// <param name="city">Идентификатор нового города</param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-city")]
        public async Task<ActionResult<ProfileOutput>> UpdateCityIdAsync(string city)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            if (Guid.TryParse(city, out Guid cityId))
            {
                return BadRequest(new { message = "Ошибка. Неправильный формат id города (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)" });
            }

            try
            {
                var result = await _userService.UpdateCityIdAsync(userId, cityId);

                return Ok(result.ToProfileOutput());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
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
