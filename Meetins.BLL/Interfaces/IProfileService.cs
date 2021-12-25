using Meetins.BLL.DTOs;
using System;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> GetUserProfile(Guid userId);
    }
}
