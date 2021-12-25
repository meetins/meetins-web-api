using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class RefreshTokenResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
