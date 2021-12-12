using Meetins.DAL.EF;
using Meetins.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private InMemoryContext _db;
        private UserRepository _userRepository;
        private AboutRepository _aboutRepository;
        private RefreshTokenRepository _refreshTokenRepository;

        public UnitOfWork()
        {
            _db = new InMemoryContext();
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_db);
                }
                return _userRepository;
            }
        }

        public IAboutRepository Abouts
        {
            get
            {
                if (_aboutRepository is null)
                {
                    _aboutRepository = new AboutRepository(_db);
                }
                return _aboutRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokens
        {
            get
            {
                if (_refreshTokenRepository is null)
                {
                    _refreshTokenRepository = new RefreshTokenRepository(_db);
                }
                return _refreshTokenRepository;
            }
        }
        public void Dispose()
        {
            _db.Dispose();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
