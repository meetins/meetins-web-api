using Meetins.BLL.DTO;
using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.AccountSettings.Requests;
using Meetins.BLL.DTOs.Requests;
using Meetins.BLL.DTOs.Responses;
using Meetins.BLL.Interfaces;
using Meetins.BLL.Options;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _db;

        public UserService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public async Task<AutheticateResponseDto> AuthenticateUser(AuthenticateRequestDto authenticateRequest)
        {
            UserEntity user = await _db.Users.IdentityUserAsync(authenticateRequest.EmailOrPhone, authenticateRequest.Password);

            var identity = GetClaimsIdentity(user);

            if (identity != null)
            {

                string accessToken = GenerateAccessToken(identity.Claims);
                string refreshToken = GenerateRerfreshToken();

                RefreshTokenEntity refreshTokenDto = new RefreshTokenEntity()
                {
                    Token = refreshToken,
                    UserId = user.UserId
                };
                await _db.RefreshTokens.Create(refreshTokenDto);
                await _db.SaveChangesAsync();

                AutheticateResponseDto autheticateResponse = new AutheticateResponseDto
                {
                    UserId = user.UserId,
                    Token = accessToken,
                    RefreshToken = refreshToken
                };

                return autheticateResponse;
            }

            return null;
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest)
        {
            bool isValidRefreshToken = ValidateRefreshToken(refreshTokenRequest.RefreshToken);

            if (!isValidRefreshToken)
            {
                return null;
            }

            RefreshTokenEntity refreshToken = await _db.RefreshTokens.GetByToken(refreshTokenRequest.RefreshToken);

            if (refreshToken is null)
            {
                return null;
            }

            UserEntity user = await _db.Users.GetUserById(refreshToken.UserId);

            if (user is null)
            {
                return null;
            }

            await _db.RefreshTokens.Delete(refreshToken);
            await _db.SaveChangesAsync();

            var identity = GetClaimsIdentity(user);

            if (identity != null)
            {

                string newAccessToken = GenerateAccessToken(identity.Claims);
                string newRefreshToken = GenerateRerfreshToken();

                RefreshTokenEntity refreshTokenDto = new RefreshTokenEntity()
                {
                    Token = newRefreshToken,
                    UserId = user.UserId
                };
                await _db.RefreshTokens.Create(refreshTokenDto);
                await _db.SaveChangesAsync();

                RefreshTokenResponseDto refreshTokenResponse = new RefreshTokenResponseDto
                {
                    NewAccessToken = newAccessToken,
                    NewRefreshToken = newRefreshToken
                };

                return refreshTokenResponse;

            }
            return null;
        }
         
        public async Task RegisterUserAsync(UserDto user)
        {
            UserEntity newUser = new UserEntity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                UserIcon = "/images/no-photo.png",
                DateRegister = DateTime.UtcNow
            };


            DateTime dt2020 = new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan tsInterval = newUser.DateRegister.Subtract(dt2020);

            newUser.LoginUrl = "id"+Convert.ToUInt64(tsInterval.TotalSeconds).ToString();



            await _db.Users.AddUserAsync(newUser);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> CheckUserByEmailOrPhoneNumber(string email, string phoneNumber)
        {
            var user = await _db.Users.GetUserByEmailOrPhoneNumber(email, phoneNumber);

            if (user is null)
            {
                return false;
            }

            return true;

        }

        public async Task<UserDto> CheckUserByEmail(string email)
        {
            UserEntity user = await _db.Users.GetUserByEmail(email);

            if (user is null)
            {
                return null;
            }

            UserDto userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                DateRegister = user.DateRegister
            };

            return userDto;
        }

        public async Task<UserDto> CheckUserByPhone(string phone)
        {
            UserEntity user = await _db.Users.GetUserByPhone(phone);

            if (user is null)
            {
                return null;
            }

            UserDto userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                DateRegister = user.DateRegister
            };

            return userDto;
        }
        public async Task<UserDto> CheckUserByLoginUrl(string loginUrl)
        {
            UserEntity user = await _db.Users.GetUserByLoginUrl(loginUrl);

            if (user is null)
            {
                return null;
            }

            UserDto userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                DateRegister = user.DateRegister
            };

            return userDto;
        }

        public async Task DeleteAllRefreshTokenByUserId(Guid userId)
        {
            await _db.RefreshTokens.DeleteAll(userId);
            await _db.SaveChangesAsync();
        }

        public async Task EditAccountSettings(EditAccountSettingsRequestDto editAccountSettingsRequest)
        {
            UserEntity userEntity = new UserEntity() 
            { 
                UserId = editAccountSettingsRequest.UserId,
                FirstName = editAccountSettingsRequest.FirstName,
                LastName = editAccountSettingsRequest.LastName,
                Email = editAccountSettingsRequest.Email,
                PhoneNumber = editAccountSettingsRequest.PhoneNumber,
                Password = editAccountSettingsRequest.Password,
                BirthDate = editAccountSettingsRequest.BirthDate,
                LoginUrl = editAccountSettingsRequest.Password
            };
            await _db.Users.UpdateUser(userEntity);
            await _db.SaveChangesAsync();
        }

        #region ТЕСТОВЫЕ МЕТОДЫ
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _db.Users.GetAllUsersAsync();

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var item in users)
            {
                userDtos.Add(new UserDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Password = item.Password,
                    Gender = item.Gender,
                    DateRegister = item.DateRegister,
                    LoginUrl = item.LoginUrl,
                    BirthDate = item.BirthDate,
                    AvatarPath = item.UserIcon
                });
            }

            return userDtos;
        }
        #endregion

        #region PRIVATE-методы
        private bool ValidateRefreshToken(string refreshToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = RefreshTokenOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = RefreshTokenOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = RefreshTokenOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }

        private ClaimsIdentity GetClaimsIdentity(UserEntity user)
        {

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("userId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName+user.LastName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "JwtToken", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AccessTokenOptions.ISSUER,
                    audience: AccessTokenOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.AddMinutes(AccessTokenOptions.LIFETIME),
                    signingCredentials: new SigningCredentials(AccessTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return accessToken;
        }

        private string GenerateRerfreshToken()
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: RefreshTokenOptions.ISSUER,
                    audience: RefreshTokenOptions.AUDIENCE,
                    notBefore: now,
                    claims: null,
                    expires: now.AddMinutes(RefreshTokenOptions.LIFETIME),
                    signingCredentials: new SigningCredentials(RefreshTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return refreshToken;
        }        
        #endregion
    }
}
