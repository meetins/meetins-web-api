using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.MainPage.Output;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Meetins.Services.MainPage
{
    public class AboutRepository : IAboutRepository
    {
        private readonly PostgreDbContext _context;

        public AboutRepository(PostgreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AboutsOutput>> GetAboutsAsync()
        {
            var result = await (from a in _context.Abouts
                                select new AboutsOutput
                                {
                                   MainText = a.MainText,
                                   Description = a.Description
                                })
                     .ToListAsync();

            return result;
        }
    }
}
