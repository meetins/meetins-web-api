using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs.Settings.Requests
{
    public class EditAccountSettingsRequestDto
    {
        public Guid UserId { get; set; }        
        public string Email { get; set; }       
        public string Password { get; set; }       
        public string LoginUrl { get; set; }
    }
}
