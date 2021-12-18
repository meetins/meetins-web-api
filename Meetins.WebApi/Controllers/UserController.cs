using Meetins.BLL.DTO;
using Meetins.BLL.DTOs.Requests;
using Meetins.BLL.DTOs.Responses;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models;
using Meetins.WebApi.Models.Requests;
using Meetins.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult<AuthenticateResponseModel>> AuthenticateUser([FromBody] AuthenticateRequestModel authenticateRequest)
        {
            AuthenticateRequestDto authenticateRequestDto = new AuthenticateRequestDto
            {
                EmailOrPhone = authenticateRequest.EmailOrPhone,
                Password = authenticateRequest.Password
            };

            AutheticateResponseDto authResult = await _userService.AuthenticateUser(authenticateRequestDto);

            if (authResult is null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            AuthenticateResponseModel response = new AuthenticateResponseModel(authResult.UserId, authResult.Token, authResult.RefreshToken);
            

            return Ok(response);
        }

        [HttpPost, Route("refresh-token")]
        public async Task<ActionResult<RefreshTokenResponseModel>> RefreshTokenAsync([FromBody] RefreshTokenRequestModel refreshTokenModel)
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

            RefreshTokenResponseModel response = new RefreshTokenResponseModel
            {
                AccessToken = refreshTokenResults.NewAccessToken,
                RefreshToken = refreshTokenResults.NewRefreshToken
            };

            return Json(response);
        }

        [HttpPost, Route("register-user")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterUserRequestModel userRequest)
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
                Password = userDto.Password
            };

            authenticateRequestDto.EmailOrPhone = userDto.Email is not null ? userDto.Email : userDto.PhoneNumber;

            AutheticateResponseDto authResult = await _userService.AuthenticateUser(authenticateRequestDto);

            var response = new
            {
                status = "User registered successfully.",
                access_token = authResult.Token,
                resresh_token = authResult.RefreshToken,
                user_id = authResult.UserId
            };

            return Json(response); 
        }

        [HttpGet,Route("check-email")]
        public async Task<ActionResult<CheckEmailResponseModel>> CheckEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { errorText = "Email cannot be null or empty." });
            }

            UserDto user = await _userService.CheckUserByEmail(email);

            CheckEmailResponseModel checkEmailResponseModel = new CheckEmailResponseModel()
            {
                Email = email
            };

            checkEmailResponseModel.IsExists = user is not null;

            return Ok(checkEmailResponseModel);
        }

        [HttpGet, Route("check-phone")]
        public async Task<ActionResult<CheckPhoneResponseModel>> CheckPhoneAsync(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return BadRequest(new { errorText = "Phone cannot be null or empty." });
            }

            UserDto user = await _userService.CheckUserByPhone(phone);

            CheckPhoneResponseModel checkPhoneResponseModel = new CheckPhoneResponseModel()
            {
                Phone = phone
            };

            checkPhoneResponseModel.IsExists = user is not null;

            return Ok(checkPhoneResponseModel);
        }

        [Authorize]
        [HttpDelete, Route("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("userId");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _userService.DeleteAllRefreshTokenByUserId(userId);

            return NoContent();
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
