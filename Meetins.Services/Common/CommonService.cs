using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.Common
{
    /// <summary>
    /// Получение списка всех городов пользователей.
    /// </summary>
    public class CommonService : ICommonService
    {
        private ICommonRepository _commonRepository;

        public CommonService(ICommonRepository commonService)
        {
            _commonRepository = commonService;
        }

        /// <summary>
        /// Получение списка всех городов пользователей.
        /// </summary>
        /// <returns> Список все городов пользователей. </returns>
        public async Task<IEnumerable<CityOutput>> GetAllCitiesAsync()
        {
            var result = await _commonRepository.GetAllCitiesAsync();

            return result;
        }
    }
}
