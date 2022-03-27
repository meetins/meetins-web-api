using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.MainPage.Output;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Meetins.Core.Logger;
using System;

namespace Meetins.Services.MainPage
{
    //TODO: переименовать в MainPageRepository
    public class AboutRepository : IAboutRepository
    {
        private PostgreDbContext _postgreDbContext;

        public AboutRepository(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        public async Task<IEnumerable<AboutsOutput>> GetAboutsAsync()
        {
            try
            {
                var result = await (from a in _postgreDbContext.Abouts
                                    select new AboutsOutput
                                    {
                                        MainText = a.MainText,
                                        Description = a.Description
                                    })
                     .ToListAsync();

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
