using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Models.AccountSettings.Responses
{
    public class CheckLoginUrlResponseModel
    {
        public string LoginUrl { get; set; }
        public bool IsExists { get; set; }
    }
}
