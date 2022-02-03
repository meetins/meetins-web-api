using Meetins.Models.Profile.Output;

namespace Meetins.Models.User.Output
{
    public class LoginOutput
    {
        public AuthenticateOutput auth { get; set; }
        public ProfileOutput profile { get; set; }
    }
}
