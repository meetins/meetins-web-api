using Meetins.Models.Entities;
using Meetins.Models.Profile.Output;

namespace Meetins.Models.Mapper
{
    /// <summary>
    /// Класс маппера сущности пользователя в модель профиля.
    /// </summary>
    public static class UserEntityToProfile
    {
        /// <summary>
        /// Метод смаппит пользователя в профиль.
        /// </summary>
        /// <param name="user">Модель пользователя.</param>
        /// <returns>Модель профиля.</returns>
        public static ProfileOutput ToProfileOutput(this UserEntity user)
        {
            if (user is null)
            {
                return null;
            }

            ProfileOutput profile= new()
            {
                UserId = user.UserId,
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
