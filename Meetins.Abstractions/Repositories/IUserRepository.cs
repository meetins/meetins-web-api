using Meetins.Models.User.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserOutput> IdentityUserAsync(string email, string password);

        Task<UserOutput> GetUserByIdAsync(Guid guid);
        
        Task<UserOutput> AddUserAsync(string name, string email, string password, string gender);

        Task<UserOutput> GetUserByEmailAsync(string email);

        Task<UserOutput> GetUserByLoginAsync(string login);

        Task<UserOutput> GetUserByPhoneAsync(string phone);

        Task<UserOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath);

        Task<UserOutput> UpdateStatusAsync(Guid userId, string status);

        Task<UserOutput> UpdateAccountSettingsAsync(Guid userId, string email, string password, string login);

        Task<UserOutput> UpdateProfileSettingsAsync(Guid userId, string name, string phone, string birthDate);
    }
}
