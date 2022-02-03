using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Models.MainPage.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.MainPage
{
    public class AboutService : IAboutService
    {
        private IAboutRepository _aboutRepository;

        public AboutService(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<IEnumerable<AboutsOutput>> GetAboutsAsync()
        {
            var result = await _aboutRepository.GetAboutsAsync();

            return result;
        }
    }
}
