using Meetins.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция репозитория всех пользователей.
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <param name="userId"> Идентификатор авторизованного пользователя. </param>
        /// <returns> Список всех существующих пользователей. </returns>
        Task<IEnumerable<PeopleOutput>> GetAllPeoplesAsync(Guid userId);
    }
}
