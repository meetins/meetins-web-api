using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.MainPage.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.MainPage
{
    public class AboutService : IAboutService
    {
        private IAboutRepository _aboutRepository;
        private PostgreDbContext _postgreDbContext;

        public AboutService(IAboutRepository aboutRepository, PostgreDbContext postgreDbContext)
        {
            _aboutRepository = aboutRepository;
            _postgreDbContext = postgreDbContext;
        }

        public async Task<IEnumerable<AboutsOutput>> GetAboutsAsync()
        {
            try
            {
                var result = await _aboutRepository.GetAboutsAsync();

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
