using System;

namespace Meetins.WebApi.Models.AccountSettings.Requests
{
    public class EditAccountSettingsRequestModel
    {
        public string FirstNameAndLastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }
        public string LoginUrl { get; set; }
    }
}
