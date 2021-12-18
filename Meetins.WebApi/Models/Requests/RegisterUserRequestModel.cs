using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Requests
{
    public class RegisterUserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
    }
}
