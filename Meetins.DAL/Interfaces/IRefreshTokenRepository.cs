using Meetins.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task Create(RefreshTokenEntity refreshToken);
        Task<RefreshTokenEntity> GetByToken(string refreshToken);
        Task Delete(RefreshTokenEntity refreshToken);
        Task<Task> DeleteAll(Guid userId);
        
    }
}
