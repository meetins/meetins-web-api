using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.DTOs.Requests
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string EmailOrPhone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
