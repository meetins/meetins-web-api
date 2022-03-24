using Meetins.Abstractions.Services;
using Meetins.Communication.Abstractions;
using Meetins.Communication.Hubs;
using Meetins.Models.Profile.Input;
using Meetins.Models.Profile.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    /// <summary>
    /// В контроллере содержится функционал для получения профиля пользователя.
    /// </summary>
    [Route("profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IProfileService _profileService;
        private IFtpService _ftpService;
        

        public ProfileController(IProfileService profileService, IFtpService ftpService)
        {
            _profileService = profileService;
            _ftpService = ftpService;            
        }

        /// <summary>
        /// Получить профиль пользователя с помощью идентификатора пользователя.
        /// </summary>
        /// <returns> Выходная модель пользователя. </returns>
        [Authorize]
        [HttpGet, Route("my-profile")]
        public async Task<ActionResult<ProfileOutput>> GetUserProfileAsync()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var profile = await _profileService.GetUserProfileAsync(userId);            

            return Ok(profile);
        }

        /// <summary>
        /// Получить профиль пользователя по логину.
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <returns> Выходная модель профиля. </returns>
        [Authorize]
        [HttpPost, Route("by-login")]
        public async Task<ActionResult<ProfileOutput>> GetProfileByLoginUrl([FromBody] string login)
        {

            var profile = await _profileService.GetUserProfileByLoginAsync(login);

            if (profile is null)
            {
                return BadRequest(new { errorText = "User with this login does not exist." });
            }

            return Ok(profile);
        }

        /// <summary>
        /// Обновить статус.
        /// </summary>
        /// <param name="statusInput"> Обновленный статус. </param>
        /// <returns> Выходная моедль профиля. </returns>
        [Authorize]
        [HttpPost, Route("update-status")]
        public async Task<ActionResult<ProfileOutput>> UpdateStatusAsync([FromBody] UpdateStatusInput statusInput)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            ProfileOutput profile = await _profileService.UpdateProfileStatusAsync(userId, statusInput.Status);

            return Ok(profile);
        }

        /// <summary>
        /// Обновить автарку.
        /// </summary>
        /// <param name="uploadedFile"> Загружаемый файл. </param>
        /// <returns> Выходная модель профиля. </returns>
        [Authorize]
        [HttpPost, Route("update-avatar")]
        public async Task<ActionResult<ProfileOutput>> UpdateAvatarAsync([FromForm] IFormFile uploadedFile)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            if (uploadedFile == null)
            {
                return BadRequest(new { errorText = "Uploaded file cannot be null." });
            }

            ProfileOutput profile = await _profileService.GetUserProfileAsync(userId);

            string newAvatarPath = await _ftpService.UpdateAvatar(profile.Avatar, uploadedFile);

            ProfileOutput res = await _profileService.UpdateAvatarPathAsync(userId, newAvatarPath);

            return Ok(res);
        }
    }
}

