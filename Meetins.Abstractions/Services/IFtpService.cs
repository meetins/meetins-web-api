using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция фтп сервиса.
    /// </summary>
    public interface IFtpService
    {
        /// <summary>
        /// Метод обновит файл с аватаром.
        /// </summary>
        /// <param name="oldAvatar">Путь к старому аватару.</param>
        /// <param name="newAvatar">Файл с новым аватаром.</param>
        /// <returns>Короткий путь к новому аватару.</returns>
        Task<string> UpdateAvatar(string oldAvatar, IFormFile newAvatar);       
    }
}
