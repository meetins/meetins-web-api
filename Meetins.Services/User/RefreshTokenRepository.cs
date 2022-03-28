using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Entities;
using Meetins.Models.User.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.User
{
    /// <summary>
    /// Класс репозитория рефреш токенов.
    /// </summary>
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private PostgreDbContext _postgreDbContext;

        public RefreshTokenRepository(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Метод удалит все рефреш токены пользователя по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Статус оперцаии.</returns>
        public async Task<bool> DeleteAllAsync(Guid userId)
        {
            try
            {
                List<RefreshTokenEntity> refreshTokens = await _postgreDbContext.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();

                foreach (var item in refreshTokens)
                {
                    _postgreDbContext.RefreshTokens.Remove(item);
                }

                await _postgreDbContext.SaveChangesAsync();

                return true;
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
        /// Метод создаст новую запись в таблице рефреш токенов.
        /// </summary>
        /// <param name="refreshToken">Рефреш токен.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Выходная модель рефреш токена.</returns>
        public async Task<RefreshTokenOutput> CreateAsync(string refreshToken, Guid userId)
        {
            try
            {
                RefreshTokenEntity newRefreshToken = new RefreshTokenEntity
                {
                    Token = refreshToken,
                    UserId = userId
                };

                await _postgreDbContext.RefreshTokens.AddAsync(newRefreshToken);

                await _postgreDbContext.SaveChangesAsync();

                RefreshTokenOutput result = new()
                {
                    Token = refreshToken,
                    UserId = userId
                };

                return result;
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
        /// Метод найдёт запись в таблице рефреш токенов по значению токена.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>Выходная модель рефреш токена.</returns>
        public async Task<RefreshTokenOutput> GetByTokenAsync(string refreshToken)
        {
            try
            {
                var result = await _postgreDbContext.RefreshTokens
                    .Where(b => b.Token.Equals(refreshToken))
                    .Select(b => new RefreshTokenOutput
                    {
                        TokenId = b.RefreshTokenId,
                        Token = b.Token,
                        UserId = b.UserId
                    })
                    .FirstOrDefaultAsync();

                return result;
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
        /// Метод удалит токен по идентификатору токена.
        /// </summary>
        /// <param name="refreshTokenId">Идентификатор токена.</param>
        /// <returns>Статус операции.</returns>
        public async Task<bool> DeleteAsync(Guid refreshTokenId)
        {
            try
            {
                var deletedToken = await _postgreDbContext.RefreshTokens
                   .Where(b => b.RefreshTokenId.Equals(refreshTokenId))
                   .FirstOrDefaultAsync();

                _postgreDbContext.RefreshTokens.Remove(deletedToken);

                await _postgreDbContext.SaveChangesAsync();

                return true;
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
