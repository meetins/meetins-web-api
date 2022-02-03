using Meetins.Models.User.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenOutput> CreateAsync(string refreshToken, Guid userId);
        Task<RefreshTokenOutput> GetByTokenAsync(string refreshToken);
        Task<Task> DeleteAsync(Guid refreshTokenId);
        Task<Task> DeleteAllAsync(Guid userId);        
    }
}
