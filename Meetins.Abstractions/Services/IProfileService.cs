using Meetins.Models.Profile.Output;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    public interface IProfileService
    {
        Task<ProfileOutput> GetUserProfileAsync(Guid userId);
        
        Task<ProfileOutput> GetUserProfileByLoginAsync(string login);

        Task<ProfileOutput> UpdateProfileStatusAsync(Guid userId, string status);

        Task<ProfileOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath);
    }
}
