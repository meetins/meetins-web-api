using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
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

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <param name="userId"> Идентификатор авторизованного пользователя. </param>
        /// <returns> Список всех существующих пользователей. </returns>
        public async Task<IEnumerable<PeopleOutput>> GetAllPeoplesAsync(Guid userId)
        {
            var result = await _peopleRepository.GetAllPeoplesAsync(userId);

            return result;
        }
    }
}
