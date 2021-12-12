using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs.Responses
{
    public class AutheticateResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }       
        public string RefreshToken { get; set; }
    }
}
