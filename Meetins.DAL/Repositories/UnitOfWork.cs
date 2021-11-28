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
        private HeaderRepository _headerRepository;

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

        public IHeaderRepository Headers
        {
            get
            {
                if (_headerRepository is null)
                {
                    _headerRepository = new HeaderRepository(_db);
                }
                return _headerRepository;
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
