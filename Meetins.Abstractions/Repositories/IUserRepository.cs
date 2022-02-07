using Meetins.Models.User.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция репозитория пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Метод идентифицирует пользователя.
        /// </summary>
        /// <param name="email">Емейл пользователя.</param>
        /// <param name="password">Паоль пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> IdentityUserAsync(string email, string password);

        /// <summary>
        /// Метод получит данные пользователя по Id.
        /// </summary>
        /// <param name="guid">Идентификатор пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> GetUserByIdAsync(Guid guid);

        /// <summary>
        /// Метод добавит пользователя в БД.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="gender">Пол.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> AddUserAsync(string name, string email, string password, string gender);

        /// <summary>
        /// Метод найдёт пользователя по емейлу.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> GetUserByEmailAsync(string email);

        /// <summary>
        /// Метод найдёт пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> GetUserByLoginAsync(string login);

        /// <summary>
        /// Метод найдёт пользователя по телефону.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> GetUserByPhoneAsync(string phone);

        /// <summary>
        /// Метод обновит путь к аватару пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="newAvatarPath">Путь к новому аватару.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath);

        /// <summary>
        /// Метод обновит статус пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="status">Новый статус.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> UpdateStatusAsync(Guid userId, string status);

        /// <summary>
        /// Метод обновит настройки аккаунта.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="login">Логин.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> UpdateAccountSettingsAsync(Guid userId, string email, string password, string login);

        /// <summary>
        /// Метод обновит настройки профиля.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="name">Имя.</param>
        /// <param name="phone">Номер телефона.</param>
        /// <param name="birthDate">Дата рождения.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> UpdateProfileSettingsAsync(Guid userId, string name, string phone, string birthDate);
    }
}
