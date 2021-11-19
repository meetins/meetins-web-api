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
    public class UserRepository : IUserRepository
    {
        private InMemoryContext _db;
        public UserRepository(InMemoryContext db)
        {
            _db = db;
        }
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return _db.Users;
        }
    }
}
