using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.User.Requests
{
    public class EditAccountRequestModel
    {
        public string FisrtNameSecondName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
