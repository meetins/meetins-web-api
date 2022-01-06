using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Settings.Requests
{
    public class EditProfileSettingsRequsetModel
    {
        public string FirstNameAndLastName { get; set; }        
        public string PhoneNumber { get; set; }       
        public string BirthDate { get; set; }        
    }
}
