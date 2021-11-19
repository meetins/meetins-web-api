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
    public class UserService : IUserService
    {
        private IUnitOfWork _db;

        public UserService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _db.Users.GetAllUsersAsync();

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
