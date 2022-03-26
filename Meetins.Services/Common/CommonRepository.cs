using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.Common
{
    /// <summary>
    /// В реппозитории содержится общий функционал.
    /// </summary>
    public class CommonRepository : ICommonRepository
    {
        private PostgreDbContext _context;

        public CommonRepository(PostgreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка всех городов.
        /// </summary>
        /// <returns> Список всех городов. </returns>
        public async Task<IEnumerable<CityOutput>> GetAllCitiesAsync()
        {
            var result = await _context.Cities
                .Select(city => new CityOutput
                {
                    CityId = city.CityId,
                    CityName = city.CityName,
                    HasKudagoEvents = city.HasKudagoEvents
                })
                .ToListAsync();

            return result;
        }
    }
}
