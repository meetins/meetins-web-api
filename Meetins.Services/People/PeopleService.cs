using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.People;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.People
{
    /// <summary>
    /// Получение списка всех пользователей.
    /// </summary>
    public class PeopleService : IPeopleService
    {
        private IPeopleRepository _peopleRepository;
        private PostgreDbContext _postgreDbContext;

        public PeopleService(IPeopleRepository peopleRepository, PostgreDbContext postgreDbContext)
        {
            _peopleRepository = peopleRepository;
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
                var result = await _peopleRepository.GetAllPeoplesAsync(userId);

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
