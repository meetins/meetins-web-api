using System;

namespace Meetins.BLL.DTOs.Profile.Request
{
    public class UpdateStatusRequestDto
    {
        public Guid UserId { get; set; }
        public string NewStatus { get; set; }
    }
}
