using Meetins.Models.Profile.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция сервиса профилей.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Метод вернёт профиль по Id.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Данные профиля.</returns>
        Task<ProfileOutput> GetUserProfileAsync(Guid userId);

        /// <summary>
        /// Метод вернёт профиль по логину.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Данные профиля.</returns>
        Task<ProfileOutput> GetUserProfileByLoginAsync(string login);

        /// <summary>
        /// Метод обновит статус пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="status">Новый статус.</param>
        /// <returns>Данные профиля.</returns>
        Task<ProfileOutput> UpdateProfileStatusAsync(Guid userId, string status);

        /// <summary>
        /// Метод обновит путь к аватару.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="newAvatarPath">Путь к новому аватару.</param>
        /// <returns>Данные профиля.</returns>
        Task<ProfileOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath);
    }
}
