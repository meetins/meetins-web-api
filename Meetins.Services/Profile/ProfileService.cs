using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Mapper;
using Meetins.Models.Profile.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Services.Profile
{
    /// <summary>
    /// Класс сервиса профиля пользователя.
    /// </summary>
    public class ProfileService : IProfileService
    {
        private IUserRepository _userRepository;
        private PostgreDbContext _postgreDbContext;
        private ICommonService _commonService;

        public ProfileService(IUserRepository userRepository, PostgreDbContext postgreDbContext, ICommonService commonService)
        {
            _userRepository = userRepository;
            _postgreDbContext = postgreDbContext;
            _commonService = commonService;
        }

        /// <summary>
        /// Получить профиль пользователя с помощью идентификатора пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns> Выходная модель пользователя. </returns>
        public async Task<ProfileOutput> GetUserProfileAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                var profile = user.ToProfileOutput();

                profile.City = await _commonService.GetCityNameAsync(user.CityId);

                return profile;
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
        /// Получить профиль пользователя по логину.
        /// </summary>
        /// <param name="login"> Логин. в</param>
        /// <returns> Выходная модель профиля. </returns>
        public async Task<ProfileOutput> GetUserProfileByLoginAsync(string login)
        {
            try
            {
                var user = await _userRepository.GetUserByLoginAsync(login);

                if (user is null)
                {
                    //TODO: бросить кастомное исключение
                    return null;
                }

                var profile = user.ToProfileOutput();

                profile.City = await _commonService.GetCityNameAsync(user.CityId);

                return profile;
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
        /// Обновить автарку.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="newAvatarPath"> Загружаемый файл. </param>
        /// <returns> Выходная модель профиля. </returns>
        public async Task<ProfileOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath)
        {
            try
            {
                var user = await _userRepository.UpdateAvatarPathAsync(userId, newAvatarPath);
                var profile = user.ToProfileOutput();

                profile.City = await _commonService.GetCityNameAsync(user.CityId);

                return profile;
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
        /// Обновить статус.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="status"> Обновленный статус. </param>
        /// <returns> Выходная модель профиля. </returns>
        public async Task<ProfileOutput> UpdateProfileStatusAsync(Guid userId, string status)
        {
            try
            {
                var user = await _userRepository.UpdateStatusAsync(userId, status);
                var profile = user.ToProfileOutput();

                profile.City = await _commonService.GetCityNameAsync(user.CityId);

                return profile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }
    }
}
