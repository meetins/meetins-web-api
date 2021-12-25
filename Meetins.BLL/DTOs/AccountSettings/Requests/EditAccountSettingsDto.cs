using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs.AccountSettings.Requests
{
    public class EditAccountSettingsRequestDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string LoginUrl { get; set; }
    }
}
