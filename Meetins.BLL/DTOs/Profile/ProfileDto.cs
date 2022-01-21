using System;

namespace Meetins.BLL.DTOs
{
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }     
        public string Gender { get; set; }       
        public string Avatar { get; set; }    
        public DateTime DateRegister { get; set; }
        public DateTime? BirthDate { get; set; }
        public string LoginUrl { get; set; }
    }
}
