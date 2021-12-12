using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
