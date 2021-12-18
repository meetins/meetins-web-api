using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class CheckEmailResponseModel
    {
        public string Email { get; set; }
        public bool IsExists { get; set; }
    }
}
