using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class ProfileResponseModel
    {        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
        /// <summary>
        /// M - мужчина, F - женщина.
        /// </summary>        
        public string Gender { get; set; }

        /// <summary>
        /// Путь к иконке пользователя.
        /// </summary>        
        public string UserIcon { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>       
        public DateTime DateRegister { get; set; }
    }
}
