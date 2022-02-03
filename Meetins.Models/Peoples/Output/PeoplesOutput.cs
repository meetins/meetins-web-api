using System;

namespace Meetins.Models.Peoples.Output
{
    public class PeoplesOutput
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }  

        public string AvatarPath { get; set; }

        public string LoginUrl { get; set; }
    }
}
