using Meetins.Models.Entities;
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
        /// <param name="birthDate">Дата рождения.</param>
        /// <param name="cityId">Идентификатор города.</param>
        /// <returns>Данные пользователя после аутентификации: профиль и токены.</returns>
        Task<LoginOutput> RegisterUserAsync(string name, string email, string password, string gender, string birthDate, string cityId);

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
        /// <returns>Статус удаления.</returns>
        Task<bool> DeleteAllRefreshTokensByUserIdAsync(Guid userId);

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
        /// <returns>Статус удаления. </returns>
        Task<bool> DeleteUserByUserIdAsync(Guid userId);

        /// <summary>
        /// Метод отправит и сохранит код в БД.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>        
        /// <returns>Статус операции.</returns>
        Task<bool> SendAndSaveAcceptCodeAsync(Guid userId);

        /// <summary>
        /// Метод подтвердит почту пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="code">Код.</param>
        /// <returns>Статус операции.</returns>
        Task<bool> ConfirmMailAsync(Guid userId, string code);
    }
}
