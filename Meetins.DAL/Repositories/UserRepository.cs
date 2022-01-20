using Meetins.DAL.EF;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddUserAsync(UserEntity user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task UpdateUser(UserEntity user)
        {
            UserEntity updatedUser = await _db.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            
            if (user.FirstName != null && user.FirstName != "")
            {
                updatedUser.FirstName = user.FirstName;
            }

            if (user.LastName != null && user.LastName != "")
            {
                updatedUser.LastName = user.LastName;
            }

            if (user.Status != null)
            {
                updatedUser.Status = user.Status;
            }

            if (user.Email != null && user.Email != "")
            {
                updatedUser.Email = user.Email;
            }

            if (user.LoginUrl != null && user.LoginUrl != "")
            {
                updatedUser.LoginUrl = user.LoginUrl;
            }

            if (user.BirthDate != null)
            {
                updatedUser.BirthDate = user.BirthDate;
            }            

            if (user.Password != null && user.Password != "")
            {
                updatedUser.Password = user.Password;
            }

            if (user.PhoneNumber != null)
            {
                updatedUser.PhoneNumber = user.PhoneNumber;
            }

            if (user.LoginUrl != null && user.LoginUrl != "")
            {
                updatedUser.LoginUrl = user.LoginUrl;
            }            
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> GetUserByEmailOrPhoneNumber(string email, string phoneNumber)
        {
            if (phoneNumber is null && email is null)
            {
                return null;
            }

            if (phoneNumber is not null && email is null)
            {
                return await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            }

            if (email is not null && phoneNumber is null)
            {
                return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            }

            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email || u.PhoneNumber == phoneNumber);

        }

        public async Task<UserEntity> GetUserById(Guid guid)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == guid);
        }

        public async Task<UserEntity> GetUserByPhone(string phone)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
        }

        public async Task<UserEntity> IdentityUserAsync(string emailOrPhone, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => (u.Email == emailOrPhone || u.PhoneNumber == emailOrPhone) && u.Password == password);
        }

        public async Task<UserEntity> GetUserByLoginUrl(string loginUrl)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.LoginUrl == loginUrl);
        }
    }
}
