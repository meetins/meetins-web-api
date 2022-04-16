using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Meetins.Abstractions.Services;
using Meetins.Abstractions.Repositories;
using Meetins.Core.Options;
using Meetins.Models.User.Output;
using Meetins.Models.Mapper;
using Meetins.Models.Entities;
using Meetins.Core.Logger;
using Meetins.Core.Data;
using Meetins.Communication.Abstractions;

namespace Meetins.Services.User
{
    /// <summary>
    /// Класс сервиса пользователей.
    /// </summary>
    public class UserService : IUserService
    {
        private PostgreDbContext _postgreDbContext;
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IMailingService _mailingService;
        private ICommonService _commonService;

        public UserService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, PostgreDbContext postgreDbContext, IMailingService mailingService, ICommonService commonService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _postgreDbContext = postgreDbContext;
            _mailingService = mailingService;
            _commonService = commonService;
        }

        /// <summary>
        /// Метод зарегистрирует пользователя.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="gender">Пол.</param>
        /// <param name="birthDate">Дата рождения.</param>
        /// <param name="cityId">Идентификатор города.</param>
        /// <returns>Данные пользователя после аутентификации: профиль и токены.</returns>
        public async Task<LoginOutput> RegisterUserAsync(string name, string email, string password, string gender, string birthDate, string cityId)
        {
            try
            {
                //ищем пользователя в БД по емейлу
                var findedUser = await _userRepository.GetUserByEmailAsync(email.ToUpperInvariant());

                if (findedUser is not null)
                {
                    throw new ArgumentException($"Пользователь с емейлом {email} уже существует! ", nameof(email));
                }

                //Добавляем нового пользователя в БД
                var user = await _userRepository.AddUserAsync(name, email, password, gender, birthDate, cityId);                

                //Получаем клеймы
                var claimsIdentity = GetClaimsIdentity(user);

                //Генерируем токен доступа
                string accessToken = GenerateAccessToken(claimsIdentity.Claims);

                //Генерируем рефреш токен
                string refreshToken = GenerateRerfreshToken();

                //Сохраняем рефреш токен в БД
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод проведёт аутентификацию пользователя.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Данные пользователя после аутентификации: профиль и токены.</returns>
        public async Task<LoginOutput> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.IdentityUserAsync(email, password);

                if (user is null)
                {
                    throw new ArgumentException($"Пользователя с таким емейлом и паролем не найдено!");
                }
                
                //удалим все старые токены пользователя
                await _refreshTokenRepository.DeleteAllAsync(user.UserId);

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод удалит все рефреш токены пользователя.
        /// </summary>
        /// <param name="userId">Идетификатор пользователя.</param>
        /// <returns>Статус удаления.</returns>
        public async Task<bool> DeleteAllRefreshTokensByUserIdAsync(Guid userId)
        {
            try
            {
                return await _refreshTokenRepository.DeleteAllAsync(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Удаление аккаунта пользователя.
        /// </summary>
        /// <param name="userId"> Id пользователя. </param>
        /// <returns> Статус удаления. </returns>
        public async Task<bool> DeleteUserByUserIdAsync(Guid userId)
        {
            try
            {
                return await _userRepository.DeleteAsync(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит токен доступа по рефреш токену.
        /// </summary>
        /// <param name="refreshToken">Рефреш токен.</param>
        /// <returns>Данные дял доступа: токены.</returns>
        public async Task<AuthenticateOutput> RefreshAccessTokenAsync(string refreshToken)
        {
            try
            {
                if (String.IsNullOrEmpty(refreshToken))
                {
                    throw new ArgumentNullException(nameof(refreshToken), $"Рефреш токен не может быть пустым или null.");
                }

                //валидируем рефреш токен
                bool isValidRefreshToken = ValidateRefreshToken(refreshToken);

                if (!isValidRefreshToken)
                {
                    throw new ArgumentException($"Рефреш токен не валиден.", nameof(refreshToken));
                }

                //ищем рефреш токен в бд
                var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

                if (token is null)
                {
                    throw new ArgumentException($"Рефреш токен отсутствует в БД.", nameof(refreshToken));
                }

                //ищем юзера по юзер айди из рефреш токена
                var user = await _userRepository.GetUserByIdAsync(token.UserId);

                if (user is null)
                {
                    throw new ArgumentException($"С рефреш токеном не ассоциируется ни один пользователь.", nameof(refreshToken));
                }

                //удаляем все рефреш токены юзера
                await _refreshTokenRepository.DeleteAllAsync(user.UserId);

                //получаем клеймы
                var claimsIdentity = GetClaimsIdentity(user);

                //генерим новые токены
                string newAccessToken = GenerateAccessToken(claimsIdentity.Claims);
                string newRefreshToken = GenerateRerfreshToken();

                var newToken = await _refreshTokenRepository.CreateAsync(newRefreshToken, user.UserId);

                AuthenticateOutput auth = new AuthenticateOutput
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newToken.Token
                };

                return auth;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод найдёт пользователя по емейлу.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод найдёт пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByLoginAsync(string login)
        {
            try
            {
                var user = await _userRepository.GetUserByLoginAsync(login);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод найдёт пользователя по телефону.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByPhoneAsync(string phone)
        {
            try
            {
                var user = await _userRepository.GetUserByPhoneAsync(phone);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит емейл.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="email">Новая почта.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateEmailAsync(Guid userId, string email)
        {
            try
            {
                var user = await _userRepository.UpdateEmailAsync(userId, email);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит пароль.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="password">Новый пароль.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdatePasswordAsync(Guid userId, string password)
        {
            try
            {
                var user = await _userRepository.UpdatePasswordAsync(userId, password);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит логин.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="login">Новый логин.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateLoginAsync(Guid userId, string login)
        {
            try
            {
                var findedUser = await _userRepository.GetUserByLoginAsync(login);

                if (findedUser is not null)
                {
                    throw new ArgumentException($"Пользователь с логином {login} уже существует!", nameof(login));
                }

                var user = await _userRepository.UpdateLoginAsync(userId, login);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит имя пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="name">Новое имя.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateNameAsync(Guid userId, string name)
        {
            try
            {
                var user = await _userRepository.UpdateNameAsync(userId, name);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит телефон пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="phone">Новый телефон.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdatePhoneNumberAsync(Guid userId, string phone)
        {
            try
            {
                var user = await _userRepository.UpdatePhoneNumberAsync(userId, phone);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит дату рождения пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="birthDate">Новая дата рождения.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateBirthDateAsync(Guid userId, DateTime birthDate)
        {
            try
            {
                var user = await _userRepository.UpdateBirthDateAsync(userId, birthDate);

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод отправит и сохранит код в БД.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>        
        /// <returns>Статус операции.</returns>
        public async Task<bool> SendAndSaveAcceptCodeAsync(Guid userId)
        {
            try
            {
                var findedUser = await _userRepository.GetUserByIdAsync(userId);
                
                if (findedUser is null)
                {
                    throw new ArgumentException($"Пользователь с ID {userId} не найден.", nameof(userId));
                }

                string acceptCode = await _commonService.GenerateCodeAsync();               

                var saveStatus = await _userRepository.SaveAcceptCodeAsync(userId, acceptCode);

                await _mailingService.SendAcceptCodeMailAsync(acceptCode, findedUser.Email);

                return saveStatus;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод подтвердит почту пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="code">Код.</param>
        /// <returns>Статус операции.</returns>
        public async Task<bool> ConfirmMailAsync(Guid userId, string code)
        {
            try
            {
                var confirmStatus = await _userRepository.ConfirmMailAsync(userId, code);

                return confirmStatus;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private ClaimsIdentity GetClaimsIdentity(UserEntity user)
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
