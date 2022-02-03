using Meetins.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Meetins.Services.Ftp
{
    public class FtpService : IFtpService
    {
        private readonly string pathToImagesDirectory = "../Meetins.WebApi/wwwroot";

        private readonly string defaultAvatar = "/images/no-photo.png";

        public Task DeleteOldAvatar(string path)
        {
            if (File.Exists(pathToImagesDirectory+path))
            {
                File.Delete(pathToImagesDirectory+path);
            }

            return Task.CompletedTask;
        }

        public async Task<string> UpdateAvatar(string oldAvatar, IFormFile newAvatar)
        {
            if (oldAvatar != defaultAvatar)
            {
                await DeleteOldAvatar(oldAvatar);
            }

            string shortpath = "/images/" + Guid.NewGuid().ToString().Replace("-", "") + newAvatar.FileName;

            // путь к папке 
            string path = pathToImagesDirectory + shortpath;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await newAvatar.CopyToAsync(fileStream);
            }

            return shortpath;
        }
    }
}
