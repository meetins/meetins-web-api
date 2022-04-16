using System;

namespace Meetins.Models.Dialogs.Input
{
    public class StartDialogInput
    {
        public Guid UserId { get; set; }
        public string MessageContent { get; set; }
    }
}
