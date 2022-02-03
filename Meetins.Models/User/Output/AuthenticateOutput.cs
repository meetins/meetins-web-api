namespace Meetins.Models.User.Output
{
    public class AuthenticateOutput    
    {        
        public string AccessToken { get; set; }       
        public string RefreshToken { get; set; }
    }
}
