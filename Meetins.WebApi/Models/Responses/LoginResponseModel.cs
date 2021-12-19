using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class LoginResponseModel
    {
        public LoginResponseModel()
        {

        }
        public AuthenticateResponseModel authenticateResponse { get; set; }
        public ProfileResponseModel profile { get; set; }
    }
}
