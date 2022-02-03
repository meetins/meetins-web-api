using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.Entities;
using Meetins.Models.User.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.User
{
    public class UserRepository : IUserRepository
    {
        private InMemoryContext _db;
        public UserRepository(InMemoryContext db)
        {
            _db = db;
        }

        public async Task<UserOutput> AddUserAsync(string name, string email, string password, string gender)
        {
            UserEntity user = new()
            {
                Name = name,
                Email = email,
                Password = password,
                Gender = gender,
                Avatar = "/images/no-photo.png",
                DateRegister = DateTime.Now,
                Status = "Дефолтный статус"                
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            UserOutput userOutput = new UserOutput
            {
                UserId = user.UserId,
                Name = name,
                Email = email,
                Gender = gender,
                Login = user.UserId.ToString(),
                DateRegister = user.DateRegister,
                Status = user.Status,
                Avatar = user.Avatar,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber
            };

            return userOutput;
        }

        public async Task<UserOutput> GetUserByEmailAsync(string email)
        {
            var result = await _db.Users
                    .Where(b => b.Email.Equals(email))
                    .Select(b => new UserOutput
                    {
                        UserId = b.UserId,
                        Name = b.Name,
                        Email = b.Email,
                        Gender = b.Gender,
                        Login = b.Login,
                        DateRegister = b.DateRegister,
                        Status = b.Status,
                        Avatar = b.Avatar,
                        BirthDate = b.BirthDate,
                        PhoneNumber = b.PhoneNumber
                    })
                    .FirstOrDefaultAsync();

            return result;
        }

        public async Task<UserOutput> GetUserByIdAsync(Guid guid)
        {
            var result = await _db.Users
                    .Where(b => b.UserId.Equals(guid))
                    .Select(b => new UserOutput
                    {
                        UserId = b.UserId,
                        Name = b.Name,
                        Email = b.Email,
                        Gender = b.Gender,
                        Login = b.Login,
                        DateRegister = b.DateRegister,
                        Status = b.Status,
                        Avatar = b.Avatar,
                        BirthDate = b.BirthDate,
                        PhoneNumber = b.PhoneNumber
                    })
                    .FirstOrDefaultAsync();

            return result;
        }

        public async Task<UserOutput> GetUserByLoginAsync(string login)
        {
            var result = await _db.Users
                    .Where(b => b.Login.Equals(login))
                    .Select(b => new UserOutput
                    {
                        UserId = b.UserId,
                        Name = b.Name,
                        Email = b.Email,
                        Gender = b.Gender,
                        Login = b.Login,
                        DateRegister = b.DateRegister,
                        Status = b.Status,
                        Avatar = b.Avatar,
                        BirthDate = b.BirthDate,
                        PhoneNumber = b.PhoneNumber
                    })
                    .FirstOrDefaultAsync();

            return result;
        }

        public async Task<UserOutput> GetUserByPhoneAsync(string phone)
        {
            var result = await _db.Users
                    .Where(b => b.PhoneNumber.Equals(phone))
                    .Select(b => new UserOutput
                    {
                        UserId = b.UserId,
                        Name = b.Name,
                        Email = b.Email,
                        Gender = b.Gender,
                        Login = b.Login,
                        DateRegister = b.DateRegister,
                        Status = b.Status,
                        Avatar = b.Avatar,
                        BirthDate = b.BirthDate,
                        PhoneNumber = b.PhoneNumber
                    })
                    .FirstOrDefaultAsync();

            return result;
        }

        public async Task<UserOutput> IdentityUserAsync(string email, string password)
        {
            var result = await _db.Users
                    .Where(b => b.Email.Equals(email) && b.Password.Equals(password))
                    .Select(b => new UserOutput
                    {
                        UserId = b.UserId,
                        Name = b.Name,
                        Email = b.Email,
                        Gender = b.Gender,
                        Login = b.Login,
                        DateRegister = b.DateRegister,
                        Status = b.Status,
                        Avatar = b.Avatar,
                        BirthDate = b.BirthDate,
                        PhoneNumber = b.PhoneNumber
                    })
                    .FirstOrDefaultAsync();

            return result;
        }

        public async Task<UserOutput> UpdateAccountSettingsAsync(Guid userId, string email, string password, string login)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

            if (user != null)
            {
                user.Email = email;
                user.Password = password;
                user.Login = login;

                await _db.SaveChangesAsync();

                UserOutput result = new()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    Login = user.Login,
                    DateRegister = user.DateRegister,
                    Status = user.Status,
                    Avatar = user.Avatar,
                    BirthDate = user.BirthDate,
                    PhoneNumber = user.PhoneNumber
                };

                return result;
            }

            return null;
        }

        public async Task<UserOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

            if (user != null)
            {
                user.Avatar = newAvatarPath;
                await _db.SaveChangesAsync();
                UserOutput result = new()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    Login = user.Login,
                    DateRegister = user.DateRegister,
                    Status = user.Status,
                    Avatar = user.Avatar,
                    BirthDate = user.BirthDate,
                    PhoneNumber = user.PhoneNumber
                };

                return result;
            }

            return null;
        }

        public async Task<UserOutput> UpdateProfileSettingsAsync(Guid userId, string name, string phone, string birthDate)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

            if (user != null)
            {
                user.Name = name;
                user.PhoneNumber = phone;
                try
                {
                    user.BirthDate = DateTime.Parse(birthDate);
                }
                catch (Exception)
                {
                    user.BirthDate = null;
                }

                await _db.SaveChangesAsync();

                UserOutput result = new()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    Login = user.Login,
                    DateRegister = user.DateRegister,
                    Status = user.Status,
                    Avatar = user.Avatar,
                    BirthDate = user.BirthDate,
                    PhoneNumber = user.PhoneNumber
                };

                return result;
            }

            return null;
        }

        public async Task<UserOutput> UpdateStatusAsync(Guid userId, string status)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

            if (user != null)
            {
                user.Status = status;

                await _db.SaveChangesAsync();

                UserOutput result = new()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Gender = user.Gender,
                    Login = user.Login,
                    DateRegister = user.DateRegister,
                    Status = user.Status,
                    Avatar = user.Avatar,
                    BirthDate = user.BirthDate,
                    PhoneNumber = user.PhoneNumber
                };

                return result;
            }

            return null;
        }
    }
}
