using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs.Responses
{
    public class RefreshTokenResponseDto
    {
        public string NewAccessToken { get; set; }
        public string NewRefreshToken { get; set; }
    }
}
