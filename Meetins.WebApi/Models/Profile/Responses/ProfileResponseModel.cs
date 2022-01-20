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

        public string Status { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
               
        public string Gender { get; set; }
            
        public string UserIcon { get; set; }
            
        public DateTime DateRegister { get; set; }

        public string LoginUrl { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
