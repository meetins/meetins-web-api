using Meetins.Models.User.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция сервиса пользователей.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Метод зарегистрирует пользователя.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="gender">Пол.</param>
        /// <returns>Данные пользователя после аутентификации: профиль и токены.</returns>
        Task<LoginOutput> RegisterUserAsync(string name, string email, string password, string gender);

        /// <summary>
        /// Метод проведёт аутентификацию пользователя.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Данные пользователя после аутентификации: профиль и токены.</returns>
        Task<LoginOutput> AuthenticateUserAsync(string email, string password);
        
        /// <summary>
        /// Метод удалит все рефреш токены пользователя.
        /// </summary>
        /// <param name="userId">Идетификатор пользователя.</param>
        /// <returns></returns>
        Task<Task> DeleteAllRefreshTokensByUserIdAsync(Guid userId);

        /// <summary>
        /// Метод обновит токен доступа по рефреш токену.
        /// </summary>
        /// <param name="refreshToken">Рефреш токен.</param>
        /// <returns>Данные дял доступа: токены.</returns>
        Task<AuthenticateOutput> RefreshAccessTokenAsync(string refreshToken);
        
        /// <summary>
        /// Метод найдёт пользователя по емейлу.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> CheckUserByEmailAsync(string email);

        /// <summary>
        /// Метод найдёт пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> CheckUserByLoginAsync(string login);

        /// <summary>
        /// Метод найдёт пользователя по телефону.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <returns>Данные пользователя.</returns>
        Task<UserOutput> CheckUserByPhoneAsync(string phone);

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

        /// <summary>
        /// Метод удалит аккаунт пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns></returns>
        Task<Task> DeleteUserByUserIdAsync(Guid userId);
    }
}
