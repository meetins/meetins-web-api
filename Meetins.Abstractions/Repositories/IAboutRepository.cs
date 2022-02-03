using Meetins.Models.MainPage.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Repositories
{
    public interface IAboutRepository
    {
        Task<IEnumerable<AboutsOutput>> GetAboutsAsync();
    }
}
