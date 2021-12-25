using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string EmailOrPhone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
