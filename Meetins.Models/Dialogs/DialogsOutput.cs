using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Communication
{
    public class DialogsOutput
    {
        public Guid Id { get; set; }

        public Guid DialogId { get; set; }

        public Guid UserId { get; set; }

        public String Status { get; set; }
    }
}
