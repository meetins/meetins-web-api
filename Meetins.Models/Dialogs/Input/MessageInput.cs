using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Models.Dialogs.Input
{
    public class MessageInput
    {
        public Guid DialogId { get; set; }
        public string Content { get; set; }
    }
}
