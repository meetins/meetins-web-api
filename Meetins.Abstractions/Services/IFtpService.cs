using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    public interface IFtpService
    {
        Task<string> UpdateAvatar(string oldAvatar, IFormFile newAvatar);       
    }
}
