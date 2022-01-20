using Meetins.BLL.DTOs;
using Meetins.WebApi.Models.Responses;

namespace Meetins.BLL.Mapping
{
    public static class DtoToViewModelMapping
    {
        public static ProfileResponseModel ToProfileResponseModel(this ProfileDto profileDto)
        {
            ProfileResponseModel profile = new ProfileResponseModel
            {
                FirstName = profileDto.FirstName,
                LastName = profileDto.LastName,
                Status = profileDto.Status,
                Email = profileDto.Email,                
                PhoneNumber = profileDto.PhoneNumber,
                Gender = profileDto.Gender,
                UserIcon = profileDto.UserIcon,
                DateRegister = profileDto.DateRegister,
                BirthDate = profileDto.BirthDate,
                LoginUrl = profileDto.LoginUrl
            };

            return profile;
        }
    }
}
