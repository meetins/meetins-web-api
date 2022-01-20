using Microsoft.AspNetCore.Http;
using System;

namespace Meetins.BLL.DTOs.Ftp.Request
{
    public class UpdateAvatarRequestDto
    {
        public Guid UserId { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
