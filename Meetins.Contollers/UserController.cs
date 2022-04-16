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
        ICommonService _commonService;

        public UserController(IUserService userService, ICommonService commonService)
        {
            _userService = userService;
            _commonService = commonService;
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
            try
            {
                AuthenticateOutput refreshTokenResults = await _userService.RefreshAccessTokenAsync(refreshToken);

                return Ok(refreshTokenResults);
            }
            catch (Exception e)
            {                
                return BadRequest(new { message = e.Message });
            }            
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
                var profile = user.ToProfileOutput();

                profile.City = await _commonService.GetCityNameAsync(user.CityId);

                return Ok(profile);
            }
            catch (Exception e)
            {                
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
            var profile = user.ToProfileOutput();

            profile.City = await _commonService.GetCityNameAsync(user.CityId);

            return Ok(profile);
        }

        /// <summary>
        /// Метод удалит все рефреш токены пользователя и выйдет из системы.
        /// </summary>
        /// <returns>Статус удаления.</returns>
        [HttpDelete]
        [Route("logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            //TODO: запомнить непротухший токен доступа, по которому ещё можно получить доступ.
            var status = await _userService.DeleteAllRefreshTokensByUserIdAsync(userId);

            return Ok(status);
        }

        /// <summary>
        /// Метод отправит код подтверждения на почту аутентифицированному пользователю и сохранит код в БД.
        /// </summary>
        /// <param name="email">Почта.</param>
        /// <returns>Статус операции.</returns>
        [HttpGet]
        [Route("send-accept-code")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<ActionResult<bool>> SendAcceptCodeMailAsync()
        {
            try
            {
                string rawUserId = HttpContext.User.FindFirst("userId").Value;

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var result = await _userService.SendAndSaveAcceptCodeAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }           
        }

        /// <summary>
        /// Метод проверит код и подтвердит почту.
        /// </summary>
        /// <param name="email">Почта.</param>
        /// <param name="code">Код.</param>
        /// <returns>Статус подтверждения.</returns>
        [HttpPost]
        [Route("confirm-mail")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<ActionResult<bool>> ConfirmMailAsync([FromBody] string code)
        {
            try
            {
                string rawUserId = HttpContext.User.FindFirst("userId").Value;

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return Unauthorized();
                }

                var result = await _userService.ConfirmMailAsync(userId, code); 

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

