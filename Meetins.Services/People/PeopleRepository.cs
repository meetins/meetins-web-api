using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.People;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.People
{
    /// <summary>
    /// В репозитории содержится функционал для работы с людьми.
    /// </summary>
    public class PeopleRepository : IPeopleRepository
    {
        private PostgreDbContext _postgreDbContext;

        public PeopleRepository(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <param name="userId"> Идентификатор авторизованного пользователя. </param>
        /// <returns> Список всех существующих пользователей. </returns>
        public async Task<IEnumerable<PeopleOutput>> GetAllPeoplesAsync(Guid userId)
        {
            try
            {
                var result = await _postgreDbContext.Users.Where(d => d.UserId != userId).Include(d => d.City)
                           .Select(d => new PeopleOutput
                           {
                               Login = d.Login,
                               UserName = d.Name,
                               UserAvatar = d.Avatar,
                               Status = d.Status,
                               Age = DateTime.Now.Year - d.BirthDate.Year,
                               City = d.City.CityName
                           })
                           .ToListAsync();

                return result;
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