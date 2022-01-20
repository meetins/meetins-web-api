using Meetins.BLL.DTOs.Ftp.Request;
using Meetins.BLL.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class BaseFtpService : IFtpService
    {
        private readonly string pathToImagesDirectory = "../Meetins.WebApi/wwwroot";

        private readonly string defaultAvatar = "/images/no-photo.png";

        public async Task<string> UploadNewAvatar(UpdateAvatarRequestDto updateAvatarRequest)
        {
            if (updateAvatarRequest.OldAvatar != defaultAvatar)
            {
                await DeleteOldAvatar(updateAvatarRequest.OldAvatar);
            }            

            string shortpath = "/images/"+ Guid.NewGuid().ToString().Replace("-", "") + updateAvatarRequest.UploadedFile.FileName;

            // путь к папке 
            string path = pathToImagesDirectory + shortpath;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await updateAvatarRequest.UploadedFile.CopyToAsync(fileStream);
            }

            return shortpath;
        }

        public Task DeleteOldAvatar(string path)
        {
            if (File.Exists(pathToImagesDirectory+path))
            {
                File.Delete(pathToImagesDirectory+path);
            }

            return Task.CompletedTask;
        }
    }
}
