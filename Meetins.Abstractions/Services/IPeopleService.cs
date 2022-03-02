using Meetins.Abstractions.Repositories;
using Meetins.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция всех пользователей.
    /// </summary>
    public interface IPeopleService
    {
        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <param name="userId"> Идентификатор авторизованного пользователя. </param>
        /// <returns> Список всех существующих пользователей. </returns>
        Task<IEnumerable<PeopleOutput>> GetAllPeoplesAsync(Guid userId);
    }
}
