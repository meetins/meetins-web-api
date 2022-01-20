using Meetins.BLL.DTOs;
using Meetins.BLL.DTOs.Profile.Request;
using Meetins.BLL.Interfaces;
using Meetins.BLL.Mapping;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private IUnitOfWork _db;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public async Task<ProfileDto> GetUserProfile(Guid userId)
        {
            UserEntity user = await _db.Users.GetUserById(userId);                      

            return user.ToProfileDto();
        }

        public async Task<ProfileDto> GetUserProfileByLoginUrl(string loginUrl)
        {
            UserEntity user = await _db.Users.GetUserByLoginUrl(loginUrl);

            if (user is null)
            {
                return null;
            }  

            return user.ToProfileDto();
        }

        public async Task<ProfileDto> UpdateAvatarPath(UpdateAvatarPathRequestDto updateAvatarPathRequest)
        {
            UserEntity userEntity = new UserEntity
            {
                UserId = updateAvatarPathRequest.UserId,
                UserIcon = updateAvatarPathRequest.NewAvatarPath
            };

            await _db.Users.UpdateUser(userEntity);
            await _db.SaveChangesAsync();

            var user = await _db.Users.GetUserById(updateAvatarPathRequest.UserId);

            return user.ToProfileDto();
        }

        public async Task<ProfileDto> UpdateProfileStatus(UpdateStatusRequestDto updateStatusRequestDto)
        {
            UserEntity userEntity = new UserEntity
            {
                UserId = updateStatusRequestDto.UserId,
                Status = updateStatusRequestDto.NewStatus
            };

            await _db.Users.UpdateUser(userEntity);
            await _db.SaveChangesAsync();

            var user = await _db.Users.GetUserById(updateStatusRequestDto.UserId);

            return user.ToProfileDto();
        }
    }
}
