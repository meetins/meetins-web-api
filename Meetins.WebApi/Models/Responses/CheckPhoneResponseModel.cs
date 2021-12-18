using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.Responses
{
    public class CheckPhoneResponseModel
    {
        public string Phone { get; set; }
        public bool IsExists { get; set; }
    }
}
