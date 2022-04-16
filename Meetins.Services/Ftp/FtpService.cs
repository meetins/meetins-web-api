using Meetins.Abstractions.Services;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Meetins.Services.Ftp
{
    /// <summary>
    /// Класс сервиса для работы с файлами.
    /// </summary>
    public class FtpService : IFtpService
    {
        private readonly string pathToImagesDirectory = "../Meetins.WebApi/wwwroot";

        private readonly string defaultAvatar = "/images/no-photo.png";

        private PostgreDbContext _postgreDbContext;

        public FtpService(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Метод удалит файл старого аватара.
        /// </summary>
        /// <param name="path">Короткий путь к старому аватару.</param>
        /// <returns></returns>
        public async Task<Task> DeleteOldAvatar(string path)
        {
            try
            {
                if (File.Exists(pathToImagesDirectory + path))
                {
                    File.Delete(pathToImagesDirectory + path);
                }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Метод обновит файл аватара.
        /// </summary>
        /// <param name="oldAvatar">Короткий путь к старому аватару.</param>
        /// <param name="newAvatar">Загружаемый аватар.</param>
        /// <returns>Короткий путь к новому аватару.</returns>
        public async Task<string> UpdateAvatar(string oldAvatar, IFormFile newAvatar)
        {
            try
            {
                //Если старый аватар не дефолтный удаляем файл.
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }
    }
}
