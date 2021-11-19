using Meetins.BLL.DTO;
using Meetins.BLL.Interfaces;
using Meetins.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    class UserService : IUserService
    {
        private IUnitOfWork _db;

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _db.Users.GetAllUsers().ToList();

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var item in users)
            {
                userDtos.Add(new UserDto
                {
                    FirstName = item.FirstName
                });
            }

            return userDtos;
        }
    }
}
