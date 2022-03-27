using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.User
{
    /// <summary>
    /// Класс репозитория пользователей.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private PostgreDbContext _db;

        public UserRepository(PostgreDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Метод добавит пользователя в БД.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="email">Почта.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="gender">Пол.</param>
        /// <param name="birthDate">Дата рождения.</param>
        /// <param name="cityId">Идентификатор города.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> AddUserAsync(string name, string email, string password, string gender, string birthDate, string cityId)
        {
            try
            {
                Guid guid = Guid.NewGuid();

                UserEntity user = new()
                {
                    UserId = guid,
                    Name = name,
                    Email = email,
                    NormalizedEmail = email.ToUpperInvariant(),
                    Password = password,
                    //TODO: calculate passwordhash
                    PasswordHash = "Gt9Yc4AiIvmsC1QQbe2RZsCIqvoYlst2xbz0Fs8aHnw=",
                    Gender = gender,
                    Avatar = "/images/no-photo.png",
                    DateRegister = DateTime.Now,
                    Status = "Дефолтный статус",
                    NormalizedLogin = guid.ToString("N").ToUpperInvariant(),
                    Login = guid.ToString("N"),
                    PhoneNumber = "телефон не добавлен",
                    BirthDate = DateTime.Parse(birthDate),
                    CityId = Guid.Parse(cityId),
                    ConfirmEmailCode = "123456"
                };

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                return user;

            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод найдёт пользователя по емейлу.
        /// </summary>
        /// <param name="email">Емейл.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException(nameof(email), $"Емейл не может быть пустым или null");
                }

                var user = await _db.Users.FirstOrDefaultAsync(b => b.NormalizedEmail.Equals(email.ToUpperInvariant()));

                return user;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод получит данные пользователя по Id.
        /// </summary>
        /// <param name="guid">Идентификатор пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByIdAsync(Guid guid)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(b => b.UserId.Equals(guid));

                return user;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод найдёт пользователя по логину.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByLoginAsync(string login)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(b => b.NormalizedLogin.Equals(login.ToUpperInvariant()));

                return user;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод найдёт пользователя по телефону.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> GetUserByPhoneAsync(string phone)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(b => b.PhoneNumber.Equals(phone));

                return user;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод идентифицирует пользователя.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> IdentityUserAsync(string email, string password)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(b => b.Email.Equals(email) && b.Password.Equals(password));

                return user;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит путь к аватару.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="newAvatarPath">Новый путь к аватару.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateAvatarPathAsync(Guid userId, string newAvatarPath)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

                if (user != null)
                {
                    user.Avatar = newAvatarPath;

                    await _db.SaveChangesAsync();
                }

                return user;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит статус пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="status">Новый статус.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateStatusAsync(Guid userId, string status)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

                if (user != null)
                {
                    user.Status = status;

                    await _db.SaveChangesAsync();
                }

                return user;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Удаление аккаунта пользователя.
        /// </summary>
        /// <param name="userId"> Id пользователя. </param>
        /// <returns> Статус удаления пользователя. </returns>
        public async Task<bool> DeleteAsync(Guid userId)
        {
            try
            {
                var userToDelete = await _db.Users
                .Where(b => b.UserId.Equals(userId))
                .FirstOrDefaultAsync();

                _db.Users.Remove(userToDelete);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод обновит емейл.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="email">Новая почта.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateEmailAsync(Guid userId, string email)
        {
            try
            {
                var findedEmail = await GetUserByEmailAsync(email);

                if (findedEmail is not null)
                {
                    throw new ArgumentException($"Пользователь с емейлом {email} уже существует!", nameof(email));
                }

                var findedUser = await GetUserByIdAsync(userId);

                if (findedUser != null)
                {
                    findedUser.Email = email;

                    await _db.SaveChangesAsync();
                }

                return findedUser;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит пароль.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="password">Новый пароль.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdatePasswordAsync(Guid userId, string password)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);

                if (user != null)
                {
                    user.Password = password;

                    await _db.SaveChangesAsync();
                }

                return user;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит логин.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="login">Новый логин.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateLoginAsync(Guid userId, string login)
        {
            try
            {
                var findedLogin = await GetUserByLoginAsync(login);

                if (findedLogin is not null)
                {
                    throw new ArgumentException($"Пользователь с логиином {login} уже существует!", nameof(login));
                }

                var findedUser = await GetUserByIdAsync(userId);

                if (findedUser != null)
                {
                    findedUser.Login = login;

                    await _db.SaveChangesAsync();
                }

                return findedUser;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит имя пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="name">Новое имя.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateNameAsync(Guid userId, string name)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);

                if (user != null)
                {
                    user.Name = name;

                    await _db.SaveChangesAsync();
                }

                return user;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит телефон пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="phone">Новый телефон.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdatePhoneNumberAsync(Guid userId, string phone)
        {
            try
            {
                var findedPhone = await GetUserByPhoneAsync(phone);

                if (findedPhone is not null)
                {
                    throw new ArgumentException($"Пользователь с телефоном {phone} уже существует!", nameof(phone));
                }

                var findedUser = await GetUserByIdAsync(userId);

                if (findedUser != null)
                {
                    findedUser.PhoneNumber = phone;

                    await _db.SaveChangesAsync();
                }

                return findedUser;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }
        
        /// <summary>
        /// Метод обновит дату рождения пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="birthDate">Новая дата рождения.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateBirthDateAsync(Guid userId, DateTime birthDate)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);

                if (user != null)
                {
                    user.BirthDate = birthDate;

                    await _db.SaveChangesAsync();
                }

                return user;
            }
            catch (Exception)
            {
                //TODO: Log
                throw;
            }
        }

        /// <summary>
        /// Метод обновит город пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cityId">Идентификатор нового города.</param>
        /// <returns>Данные пользователя.</returns>
        public async Task<UserEntity> UpdateCityIdAsync(Guid userId, Guid cityId)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);

                if (user != null)
                {
                    user.CityId = cityId;

                    await _db.SaveChangesAsync();
                }

                return user;
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }
    }
}
