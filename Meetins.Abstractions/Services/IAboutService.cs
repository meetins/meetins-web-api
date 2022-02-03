using Meetins.Models.MainPage.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    public interface IAboutService
    {
        Task<IEnumerable<AboutsOutput>> GetAboutsAsync();
    }    
}
