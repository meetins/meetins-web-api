using Meetins.Models.Entities;
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
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> IdentityUserAsync(string email, string password);

        /// <summary>
        /// Метод получит данные пользователя по Id.
        /// </summary>
        /// <param name="guid">Идентификатор пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> GetUserByIdAsync(Guid guid);

        /// <summary>
        /// Метод добавит пользователя в БД.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="gender">Пол.</param>
        /// <param name="birthDate">Дата рождения.</param>
        /// <param name="cityId">Идентификатор города.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> AddUserAsync(string name, string email, string password, string gender, string birthDate, string cityId);

        /// <summary>
        /// Метод найдёт пользователя по емейлу.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> GetUserByEmailAsync(string email);

        /// <summary>
        /// Метод найдёт пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> GetUserByLoginAsync(string login);

        /// <summary>
        /// Метод найдёт пользователя по телефону.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> GetUserByPhoneAsync(string phone);

        /// <summary>
        /// Метод обновит путь к аватару пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="newAvatarPath">Путь к новому аватару.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdateAvatarPathAsync(Guid userId, string newAvatarPath);

        /// <summary>
        /// Метод обновит статус пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="status">Новый статус.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdateStatusAsync(Guid userId, string status);

        /// <summary>
        /// Метод обновит емейл.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="email">Новая почта.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdateEmailAsync(Guid userId, string email);

        /// <summary>
        /// Метод обновит пароль.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="password">Новый пароль.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdatePasswordAsync(Guid userId, string password);

        /// <summary>
        /// Метод обновит логин.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="login">Новый логин.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdateLoginAsync(Guid userId, string login);

        /// <summary>
        /// Метод обновит имя пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="name">Новое имя.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdateNameAsync(Guid userId, string name);

        /// <summary>
        /// Метод обновит телефон пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="phone">Новый телефон.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdatePhoneNumberAsync(Guid userId, string phone);

        /// <summary>
        /// Метод обновит дату рождения пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="birthDate">Новая дата рождения.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserEntity> UpdateBirthDateAsync(Guid userId, DateTime birthDate);

        /// <summary>
        /// Метод удалит аккаунт пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid userId);
    }
}
