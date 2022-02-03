using Meetins.Abstractions.Services;
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
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IProfileService _profileService;

        public UserController(IUserService userService, IProfileService profileService)
        {
            _userService = userService;
            _profileService = profileService;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult<LoginOutput>> Login([FromBody] LoginInput loginInput)
        {
            LoginOutput authResult = await _userService.AuthenticateUserAsync(loginInput.Email, loginInput.Password);

            if (authResult is null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            return Ok(authResult);
        }

        [HttpPost, Route("refresh-token")]
        public async Task<ActionResult<AuthenticateOutput>> RefreshTokenAsync([FromBody] string refreshToken)
        {

            AuthenticateOutput refreshTokenResults = await _userService.RefreshAccessTokenAsync(refreshToken);

            if (refreshTokenResults is null)
            {
                return BadRequest(new { errortext = "Invalid refresh token." });
            }

            return Ok(refreshTokenResults);
        }

        [HttpPost, Route("register-user")]
        public async Task<ActionResult<LoginOutput>> RegisterUserAsync([FromBody] RegisterUserInput registerUserInput)
        {
            var user = await _userService.CheckUserByEmailAsync(registerUserInput.Email);

            if (user != null)
            {
                return BadRequest(new { errortext = "User already exists." });
            }

            var result = await _userService.RegisterUserAsync(registerUserInput.Name, registerUserInput.Email, registerUserInput.Password, registerUserInput.Gender);            

            return Ok(result);
        }

        [HttpGet, Route("check-email")]
        public async Task<ActionResult<UserOutput>> CheckEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { errorText = "Email cannot be null or empty." });
            }

            var user = await _userService.CheckUserByEmailAsync(email);

            return Ok(user);
        }

        [HttpGet, Route("check-phone")]
        public async Task<ActionResult<UserOutput>> CheckPhoneAsync(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return BadRequest(new { errorText = "Email cannot be null or empty." });
            }

            var user = await _userService.CheckUserByPhoneAsync(phone);

            return Ok(user);
        }

        [Authorize]
        [HttpDelete, Route("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _userService.DeleteAllRefreshTokensByUserIdAsync(userId);

            return NoContent();
        }
    }
}
