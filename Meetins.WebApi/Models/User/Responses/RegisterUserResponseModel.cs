using Meetins.WebApi.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.User.Responses
{
    public class RegisterUserResponseModel
    {
        public RegisterUserResponseModel()
        {

        }
        public AuthenticateResponseModel authenticateResponse { get; set; }
        public ProfileResponseModel profile { get; set; }
    }
}
