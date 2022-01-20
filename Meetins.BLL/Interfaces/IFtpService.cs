using Meetins.BLL.DTOs.Ftp.Request;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IFtpService
    {
        Task<string> UploadNewAvatar(UpdateAvatarRequestDto updateAvatarRequest);       
    }
}
