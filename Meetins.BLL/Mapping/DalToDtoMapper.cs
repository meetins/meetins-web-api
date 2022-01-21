using Meetins.BLL.DTOs;
using Meetins.DAL.Entities;

namespace Meetins.BLL.Mapping
{
    public static class DalToDtoMapper
    {
        public static ProfileDto ToProfileDto(this UserEntity userEntity)
        {
            ProfileDto profileDto = new ProfileDto
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Status = userEntity.Status,
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber,
                Gender = userEntity.Gender,
                Avatar = userEntity.UserIcon,
                DateRegister = userEntity.DateRegister,
                BirthDate = userEntity.BirthDate,
                LoginUrl = userEntity.LoginUrl
            };

            return profileDto;
        }
    }
}
