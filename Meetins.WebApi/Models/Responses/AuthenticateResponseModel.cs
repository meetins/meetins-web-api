using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class AuthenticateResponseModel
    { 
        public string Email { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponseModel(string email, string jwtToken, string refreshToken)
        {
            Email = email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
