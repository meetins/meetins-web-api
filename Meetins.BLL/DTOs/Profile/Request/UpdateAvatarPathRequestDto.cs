using System;

namespace Meetins.BLL.DTOs.Profile.Request
{
    public class UpdateAvatarPathRequestDto
    {
        public Guid UserId { get; set; }
        public string NewAvatarPath { get; set; }
    }
}
