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
    public class HeaderRepository : IHeaderRepository
    {
        private InMemoryContext _db;
        public HeaderRepository(InMemoryContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<HeaderEntity>> GetAllHeadersAsync()
        {
            return await _db.Headers.ToListAsync();
        }
    }
}
