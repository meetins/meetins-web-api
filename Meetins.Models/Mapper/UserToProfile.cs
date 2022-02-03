using Meetins.Models.Profile.Output;
using Meetins.Models.User.Output;

namespace Meetins.Models.Mapper
{
    public static class UserToProfile
    {
        public static ProfileOutput ToProfileOutput(this UserOutput user)
        {
            ProfileOutput profile= new ProfileOutput
            {
                Name = user.Name,
                Status = user.Status,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Gender = user.Gender,
                Avatar = user.Avatar,
                DateRegister = user.DateRegister,
                Login = user.Login,
                BirthDate = user.BirthDate
            };

            return profile;
        }
    }
}
