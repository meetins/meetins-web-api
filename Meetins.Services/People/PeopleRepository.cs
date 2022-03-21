using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
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
        private PostgreDbContext _context;

        public PeopleRepository(PostgreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <param name="userId"> Идентификатор авторизованного пользователя. </param>
        /// <returns> Список всех существующих пользователей. </returns>
        public async Task<IEnumerable<PeopleOutput>> GetAllPeoplesAsync(Guid userId)
        {
            var result = await _context.Users.Where(d => d.UserId != userId).Include(d =>d.City)                        
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
    }
}