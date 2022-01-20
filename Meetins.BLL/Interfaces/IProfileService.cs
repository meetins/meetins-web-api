using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.Profile.Request;
using System;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> GetUserProfile(Guid userId);
        
        Task<ProfileDto> GetUserProfileByLoginUrl(string loginUrl);

        Task<ProfileDto> UpdateProfileStatus(UpdateStatusRequestDto updateStatusRequest);
    }
}
