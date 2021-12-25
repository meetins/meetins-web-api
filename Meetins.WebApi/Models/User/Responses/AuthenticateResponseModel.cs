using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class AuthenticateResponseModel    { 
       
        public string AccessToken { get; set; }       
        public string RefreshToken { get; set; }

        public AuthenticateResponseModel()
        {

        }
        public AuthenticateResponseModel(string accessToken, string refreshToken)
        {            
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
