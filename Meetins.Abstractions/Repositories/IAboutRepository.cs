using Meetins.Models.MainPage.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция репозитория описания проекта.
    /// </summary>
    public interface IAboutRepository
    {
        /// <summary>
        /// Метод вернет список блоков описания проекта.
        /// </summary>
        /// <returns>Список блоков описания проекта.</returns>
        Task<IEnumerable<AboutsOutput>> GetAboutsAsync();
    }
}
