using Meetins.BLL.DTO;
using Meetins.BLL.DTOs.Requests;
using Meetins.BLL.DTOs.Responses;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models;
using Meetins.WebApi.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Route("get-users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            return Ok(result);
        }

        [HttpPost, Route("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUser([FromBody] AuthenticateRequestModel authenticateRequest)
        {
            AuthenticateRequestDto authenticateRequestDto = new AuthenticateRequestDto
            {
                Email = authenticateRequest.Email,
                Password = authenticateRequest.Password
            };

            AutheticateResponseDto authResult = await _userService.AuthenticateUser(authenticateRequestDto);

            if (authResult is null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            var response = new
            {
                access_token = authResult.Token,
                resresh_token = authResult.RefreshToken,
                user_email = authenticateRequest.Email
            };

            return Json(response);
        }

        [HttpPost, Route("refresh-token")]
        public async Task<ActionResult<string>> RefreshTokenAsync([FromBody] RefreshTokenRequestModel refreshTokenModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            RefreshTokenRequestDto refreshTokenRequestDto = new RefreshTokenRequestDto
            {               
                RefreshToken = refreshTokenModel.RefreshToken
            };

            RefreshTokenResponseDto refreshTokenResults = await _userService.RefreshTokenAsync(refreshTokenRequestDto);

            if (refreshTokenResults is null)
            {
                return BadRequest(new { errortext = "Invalid refresh token." });
            }

            var response = new
            {
                access_token = refreshTokenResults.NewAccessToken,
                resresh_token = refreshTokenResults.NewRefreshToken               
            };

            return Json(response);
        }

        [HttpPost, Route("register-user")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterUserRequest userRequest)
        {
            if (userRequest == null)
            {
                return BadRequest();
            }

            bool isUserExists = await _userService.CheckUserByEmailOrPhoneNumber(userRequest.Email, userRequest.PhoneNumber);

            if (isUserExists)
            {
                return BadRequest(new { errortext = "User already exists." });
            }

            UserDto userDto = new UserDto
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                PhoneNumber = userRequest.PhoneNumber,
                Email = userRequest.Email,
                Password = userRequest.Password,
                Gender = userRequest.Gender,
                DateRegister = DateTime.UtcNow
            };
            await _userService.RegisterUserAsync(userDto);

            AuthenticateRequestDto authenticateRequestDto = new AuthenticateRequestDto
            {
                Email = userDto.Email,
                Password = userDto.Password
            };

            AutheticateResponseDto authResult = await _userService.AuthenticateUser(authenticateRequestDto);

            var response = new
            {
                status = "user registered successfully",
                access_token = authResult.Token,
                resresh_token = authResult.RefreshToken,
                user_email = authenticateRequestDto.Email
            };

            return Json(response); 
        }

        [Authorize]
        [HttpGet, Route("get-private-content")]
        public ActionResult<string> GetContent()
        {

            var content = new
            {
                content = "content"
            };
            return Json(content);
        }
    }
}
