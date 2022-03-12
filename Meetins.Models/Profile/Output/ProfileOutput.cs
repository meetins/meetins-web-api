using System;

namespace Meetins.Models.Profile.Output
{
    public class ProfileOutput
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }        

        public string Status { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
               
        public string Gender { get; set; }
            
        public string Avatar { get; set; }
            
        public DateTime DateRegister { get; set; }

        public string Login { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
