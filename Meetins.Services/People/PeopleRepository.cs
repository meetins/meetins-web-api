using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.People;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Services.People
{
    /// <summary>
    /// В реппозитории содержится функционал для получения списка пользователей.
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
            var result = await _context.Users.Where(d => d.UserId != userId)
                           .Select(d => new PeopleOutput
                           {
                               UserId = d.UserId,
                               UserName = d.Name,
                               UserAvatar = d.Avatar,
                               Status = d.Status,
                               Age = DateTime.Now.Year - d.BirthDate.Value.Year,
                               City = d.City
                           })
                           .ToListAsync();

            return result;
        }
    }
}