using Meetins.BLL.DTOs;
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
    }
}
