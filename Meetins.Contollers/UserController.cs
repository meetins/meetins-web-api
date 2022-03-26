using Meetins.Abstractions.Services;
using Meetins.Models.Mapper;
using Meetins.Models.Profile.Output;
using Meetins.Models.User.Input;
using Meetins.Models.User.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    /// <summary>
    /// В контроллере содержится функционал для регистрации, проверки учетных данных, аутентификации, авторизации, входа в систему и выхода.
    /// </summary>
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Метод проводит аутентификацию пользователя.
        /// </summary>
        /// <param name="loginInput">Входная модель для аутентификации.</param>
        /// <returns>Выходная модель после аутентификации.</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginOutput>> Login([FromBody] LoginInput loginInput)
        {
            try
            {
                LoginOutput authResult = await _userService.AuthenticateUserAsync(loginInput.Email, loginInput.Password);

                return Ok(authResult);
            }
            catch (Exception e)
            {
                //TODO: log
                return BadRequest(new { message = e.Message});
            }
        }

        /// <summary>
        /// Метод обновит токен доступа по рефреш токену.
        /// </summary>
        /// <param name="refreshToken">Рефреш токен.</param>
        /// <returns>Токен доступа и рефреш токен.</returns>
        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticateOutput>> RefreshTokenAsync([FromBody] string refreshToken)
        {
            //TODO: отрефакторить
            AuthenticateOutput refreshTokenResults = await _userService.RefreshAccessTokenAsync(refreshToken);

            if (refreshTokenResults is null)
            {
                return BadRequest(new { errortext = "Invalid refresh token." });
            }

            return Ok(refreshTokenResults);
        }

        /// <summary>
        /// Метод зарегистрирует пользователя.
        /// </summary>
        /// <param name="registerUserInput">Входная модель регистрации пользователя.</param>
        /// <returns>Выходные данные: токены и профиль. </returns>
        [HttpPost]
        [Route("register-user")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginOutput>> RegisterUserAsync([FromBody] RegisterUserInput registerUserInput)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(registerUserInput.Name,
                                                              registerUserInput.Email,
                                                              registerUserInput.Password,
                                                              registerUserInput.Gender,
                                                              registerUserInput.BirthDate,
                                                              registerUserInput.CityId);

                return Ok(result);
            }
            catch (Exception e)
            {
                //TODO: log
                return BadRequest(new { message = e.Message });                
            }            
        }

        /// <summary>
        /// Метод вернёт профиль пользователя по емейлу, если таков существует.
        /// </summary>
        /// <param name="email">Почта.</param>
        /// <returns>Данные профиля пользователя.</returns>
        [HttpPost]
        [Route("check-email")]
        [AllowAnonymous]
        public async Task<ActionResult<ProfileOutput>> CheckEmailAsync([FromBody] string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);

                return Ok(user.ToProfileOutput());
            }
            catch (Exception e)
            {
                //TODO: log
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Метод вернёт профиль пользователя по телефону, если таков существует.
        /// </summary>
        /// <param name="phone">Телефон.</param>
        /// <returns>Данные профиля пользователя.</returns>
        [HttpPost]
        [Route("check-phone")]
        [AllowAnonymous]
        public async Task<ActionResult<ProfileOutput>> CheckPhoneAsync([FromBody] string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return BadRequest(new { errorText = "Email cannot be null or empty." });
            }

            var user = await _userService.GetUserByPhoneAsync(phone);

            return Ok(user.ToProfileOutput());
        }

        /// <summary>
        /// Метод удалит все рефреш токены пользователя.
        /// </summary>
        /// <returns>Статус удаления.</returns>
        [HttpDelete]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var status = await _userService.DeleteAllRefreshTokensByUserIdAsync(userId);

            return Ok(status);
        }
    }
}
