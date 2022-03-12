using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Models.Mapper;
using Meetins.Models.Profile.Output;
using System;
using System.Threading.Tasks;

namespace Meetins.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ProfileOutput> GetUserProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            
            return user.ToProfileOutput();
        }

        public async Task<ProfileOutput> GetUserProfileByLoginAsync(string login)
        {
            var user = await _userRepository.GetUserByLoginAsync(login);
            
            if (user is null)
            {
                return null;
            }

            return user.ToProfileOutput();
        }
       
        public async Task<ProfileOutput> UpdateAvatarPathAsync(Guid userId, string newAvatarPath)
        {
            var user = await _userRepository.UpdateAvatarPathAsync(userId, newAvatarPath);         

            return user.ToProfileOutput();
        }

        public async Task<ProfileOutput> UpdateProfileStatusAsync(Guid userId, string status)
        {
            var user = await _userRepository.UpdateStatusAsync(userId, status);

            return user.ToProfileOutput();
        }
    }
}
