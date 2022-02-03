using Meetins.Models.User.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    public interface IUserService
    {
        Task<LoginOutput> RegisterUserAsync(string name, string email, string password, string gender);

        Task<LoginOutput> AuthenticateUserAsync(string email, string password);
        
        Task<Task> DeleteAllRefreshTokensByUserIdAsync(Guid userId);

        Task<AuthenticateOutput> RefreshAccessTokenAsync(string refreshToken);
        
        Task<UserOutput> CheckUserByEmailAsync(string email);

        Task<UserOutput> CheckUserByLoginAsync(string login);

        Task<UserOutput> CheckUserByPhoneAsync(string phone);

        Task<UserOutput> UpdateAccountSettingsAsync(Guid userId, string email, string password, string login);

        Task<UserOutput> UpdateProfileSettingsAsync(Guid userId, string name, string phone, string birthDate);
    }
}
