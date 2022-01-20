using Meetins.BLL.DTOs;
using Meetins.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Mapping
{
    public static class UserEntityMapping
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
                UserIcon = userEntity.UserIcon,
                DateRegister = userEntity.DateRegister,
                BirthDate = userEntity.BirthDate,
                LoginUrl = userEntity.LoginUrl
            };

            return profileDto;
        }
    }
}
