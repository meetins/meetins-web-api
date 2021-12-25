using Meetins.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();

        Task<UserEntity> IdentityUserAsync(string email, string password);

        Task<UserEntity> GetUserById(Guid guid);

        Task AddUserAsync(UserEntity user);

        Task<UserEntity> GetUserByEmailOrPhoneNumber(string email, string phoneNumber);

        Task<UserEntity> GetUserByEmail(string email);
        Task<UserEntity> GetUserByLoginUrl(string loginUrl);
        Task<UserEntity> GetUserByPhone(string phone);

        Task UpdateUser(UserEntity user);
    }
}
