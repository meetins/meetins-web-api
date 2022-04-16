using System;

namespace Meetins.Communication.Models
{
    public class NotifyInput
    {
        public Guid DialogId { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string MessageContent { get; set; }
    }
}
