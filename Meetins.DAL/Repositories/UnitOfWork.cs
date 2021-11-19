using Meetins.DAL.EF;
using Meetins.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Repositories
{
    class UnitOfWork : IUnitOfWork
    {
        private InMemoryContext _db;
        private UserRepository _userRepository;


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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.DisposeAsync();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChangesAsync()
        {
            _db.SaveChangesAsync();
        }
    }
}
