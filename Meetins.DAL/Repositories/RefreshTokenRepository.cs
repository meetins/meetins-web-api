using Meetins.DAL.EF;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Repositories
{
    class RefreshTokenRepository : IRefreshTokenRepository
    {
        private InMemoryContext _db;
        public RefreshTokenRepository(InMemoryContext db)
        {
            _db = db;
        }

        public Task<RefreshTokenEntity> GetByToken(string refreshToken)
        {
            RefreshTokenEntity refreshTokenEntity = _db.RefreshTokens.FirstOrDefault(u => u.Token == refreshToken);

            return Task.FromResult(refreshTokenEntity);
        }

        public Task Create(RefreshTokenEntity refreshToken)
        {
            refreshToken.RefreshTokenId = new Guid();

            _db.RefreshTokens.Add(refreshToken);

            return Task.CompletedTask;
        }

        public Task Delete(RefreshTokenEntity refreshToken)
        {
            _db.RefreshTokens.Remove(refreshToken);

            return Task.CompletedTask;
        }
    }
}
