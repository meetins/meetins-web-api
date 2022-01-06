using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs.Settings.Requests
{
    public class EditProfileSettingsRequestDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string PhoneNumber { get; set; }        
        public DateTime? BirthDate { get; set; }       
    }
}
