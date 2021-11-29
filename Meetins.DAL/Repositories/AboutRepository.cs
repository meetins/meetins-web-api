using Meetins.DAL.EF;
using Meetins.DAL.Entities;
using Meetins.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Repositories
{
    public class AboutRepository : IAboutRepository
    {
        private InMemoryContext _db;
        public AboutRepository(InMemoryContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<AboutEntity>> GetAboutsAsync()
        {
            return await _db.Abouts.ToListAsync();
        }
    }
}
