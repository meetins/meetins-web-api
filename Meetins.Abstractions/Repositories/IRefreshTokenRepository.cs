using Meetins.Models.User.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция репозитория рефреш токенов пользователей.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Метод создаст в таблице новую запись.
        /// </summary>
        /// <param name="refreshToken">Значение рефреш токена.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Созданная запись.</returns>
        Task<RefreshTokenOutput> CreateAsync(string refreshToken, Guid userId);

        /// <summary>
        /// Метод найдет в таблице рефреш токенов запись по значению токена.
        /// </summary>
        /// <param name="refreshToken">Значение рефреш токена.</param>
        /// <returns>Запись о рефреш токене.</returns>
        Task<RefreshTokenOutput> GetByTokenAsync(string refreshToken);

        /// <summary>
        /// Метод удалит рефреш токен.
        /// </summary>
        /// <param name="refreshTokenId">Идентификатор токена.</param>
        /// <returns></returns>
        Task<Task> DeleteAsync(Guid refreshTokenId);

        /// <summary>
        /// Метод удалит все токены принадлежащие пользователю.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns></returns>
        Task<Task> DeleteAllAsync(Guid userId);        
    }
}
