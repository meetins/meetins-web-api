using System;

namespace Meetins.WebApi.Models.Settings.Requests
{
    public class EditAccountSettingsRequestModel
    {       
        public string Email { get; set; }       
        public string Password { get; set; }        
        public string LoginUrl { get; set; }
    }
}
