using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.Entities;
using Meetins.Models.User.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.User
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private InMemoryContext _db;

        public RefreshTokenRepository(InMemoryContext db)
        {
            _db = db;
        }

        public async Task<bool> DeleteAllAsync(Guid userId)
        {
            try
            {
                List<RefreshTokenEntity> refreshTokens = await _db.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();

                foreach (var item in refreshTokens)
                {
                    _db.RefreshTokens.Remove(item);

                }

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<RefreshTokenOutput> CreateAsync(string refreshToken, Guid userId)
        {
            RefreshTokenEntity newRefreshToken = new RefreshTokenEntity
            {
                Token = refreshToken,
                UserId = userId
            };

            await _db.RefreshTokens.AddAsync(newRefreshToken);

            await _db.SaveChangesAsync();

            RefreshTokenOutput result = new()
            {
                Token = refreshToken,
                UserId = userId
            };

            return result;
        }

        public async Task<RefreshTokenOutput> GetByTokenAsync(string refreshToken)
        {
            var result = await _db.RefreshTokens
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

        public async Task<Task> DeleteAsync(Guid refreshTokenId)
        {

            var deletedToken = await _db.RefreshTokens
                    .Where(b => b.RefreshTokenId.Equals(refreshTokenId))
                    .FirstOrDefaultAsync();

            _db.RefreshTokens.Remove(deletedToken);

            await _db.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}
