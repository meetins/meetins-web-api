using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
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

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получить профиль пользователя с помощью идентификатора пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns> Выходная модель пользователя. </returns>
        public async Task<ProfileOutput> GetUserProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            
            return user.ToProfileOutput();
        }

        /// <summary>
        /// Получить профиль пользователя по логину.
        /// </summary>
        /// <param name="login"> Логин. в</param>
        /// <returns> Выходная модель профиля. </returns>
        public async Task<ProfileOutput> GetUserProfileByLoginAsync(string login)
        {
            var user = await _userRepository.GetUserByLoginAsync(login);
            
            if (user is null)
            {
                return null;
            }

            return user.ToProfileOutput();
        }

        /// <summary>
        /// Обновить автарку.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="newAvatarPath"> Загружаемый файл. </param>
        /// <returns> Выходная модель профиля. </returns>
        public async Task<ProfileOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath)
        {
            var user = await _userRepository.UpdateAvatarPathAsync(userId, newAvatarPath);         

            return user.ToProfileOutput();
        }

        /// <summary>
        /// Обновить статус.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="status"> Обновленный статус. </param>
        /// <returns> Выходная моедль профиля. </returns>
        public async Task<ProfileOutput> UpdateProfileStatusAsync(Guid userId, string status)
        {
            var user = await _userRepository.UpdateStatusAsync(userId, status);

            return user.ToProfileOutput();
        }
    }
}
