using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class AuthenticateResponseModel
    { 
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }       
        public string RefreshToken { get; set; }

        public AuthenticateResponseModel(Guid userId, string accessToken, string refreshToken)
        {
            UserId = userId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
