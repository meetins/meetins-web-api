using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Meetins.Abstractions.Services;
using Meetins.Abstractions.Repositories;
using Meetins.Core.Options;
using Meetins.Models.User.Output;
using Meetins.Models.Mapper;

namespace Meetins.Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;

        public UserService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginOutput> RegisterUserAsync(string name, string email, string password, string gender)
        {
            var user = await _userRepository.AddUserAsync(name, email, password, gender);

            if (user is null)
            {
                return null;
            }

            var claimsIdentity = GetClaimsIdentity(user);

            string accessToken = GenerateAccessToken(claimsIdentity.Claims);

            string refreshToken = GenerateRerfreshToken();

            await _refreshTokenRepository.CreateAsync(refreshToken, user.UserId);

            AuthenticateOutput authenticateOutput = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            LoginOutput loginOutput = new LoginOutput
            {
                auth = authenticateOutput,
                profile = user.ToProfileOutput()
            };

            return loginOutput;
        }

        public async Task<LoginOutput> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.IdentityUserAsync(email, password);

            if (user is null)
            {
                return null;
            }

            var claimsIdentity = GetClaimsIdentity(user);

            string accessToken = GenerateAccessToken(claimsIdentity.Claims);

            string refreshToken = GenerateRerfreshToken();

            await _refreshTokenRepository.CreateAsync(refreshToken, user.UserId);

            AuthenticateOutput authenticateOutput = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            LoginOutput loginOutput = new LoginOutput
            {
                auth = authenticateOutput,
                profile = user.ToProfileOutput()
            };

            return loginOutput;
        }

        public async Task<Task> DeleteAllRefreshTokensByUserIdAsync(Guid userId)
        {
            await _refreshTokenRepository.DeleteAllAsync(userId);

            return Task.CompletedTask;
        }

        public async Task<AuthenticateOutput> RefreshAccessTokenAsync(string refreshToken)
        {
            //валидируем рефреш токен
            bool isValidRefreshToken = ValidateRefreshToken(refreshToken);

            if (!isValidRefreshToken)
            {
                return null;
            }

            //иищем рефреш токен в бд
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (token is null)
            {
                return null;
            }

            //ищем юзера по юзер айди из рефреш токена
            var user = await _userRepository.GetUserByIdAsync(token.UserId);

            if (user is null)
            {
                return null;
            }

            //удаляем рефреш токен
            await _refreshTokenRepository.DeleteAsync(token.TokenId);


            var claimsIdentity = GetClaimsIdentity(user);

            //генерим новые токены
            string newAccessToken = GenerateAccessToken(claimsIdentity.Claims);
            string newRefreshToken = GenerateRerfreshToken();

            var newToken = _refreshTokenRepository.CreateAsync(newRefreshToken, user.UserId);

            AuthenticateOutput auth = new AuthenticateOutput
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return auth;
        }

        public async Task<UserOutput> CheckUserByEmailAsync(string email)
        {
            var res = await _userRepository.GetUserByEmailAsync(email);

            return res;
        }

        public async Task<UserOutput> CheckUserByLoginAsync(string login)
        {
            var res = await _userRepository.GetUserByLoginAsync(login);

            return res;
        }

        public async Task<UserOutput> CheckUserByPhoneAsync(string phone)
        {
            var res = await _userRepository.GetUserByPhoneAsync(phone);

            return res;
        }

        public async Task<UserOutput> UpdateAccountSettingsAsync(Guid userId, string email, string password, string login)
        {
            var res = await _userRepository.UpdateAccountSettingsAsync(userId, email, password, login);

            return res;
        }

        public async Task<UserOutput> UpdateProfileSettingsAsync(Guid userId, string name, string phone, string birthDate)
        {
            var res = await _userRepository.UpdateAccountSettingsAsync(userId, name, phone, birthDate);

            return res;
        }

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

        private ClaimsIdentity GetClaimsIdentity(UserOutput user)
        {
            var claims = new List<Claim>
                {
                    new Claim("userId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "JwtToken", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
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
