using Meetins.BLL.DTOs.Ftp.Request;
using Meetins.BLL.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class BaseFtpService : IFtpService
    {
        private readonly string pathToImagesDirectory = "../Meetins.WebApi/wwwroot";

        public async Task<string> UploadFile(UpdateAvatarRequestDto updateAvatarRequest)
        {           
            string shortpath = "/images/"+ updateAvatarRequest.UserId.ToString().Replace("-", "") + updateAvatarRequest.UploadedFile.FileName;

            // путь к папке 
            string path = pathToImagesDirectory + shortpath;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await updateAvatarRequest.UploadedFile.CopyToAsync(fileStream);
            }

            return shortpath;
        }
    }
}
