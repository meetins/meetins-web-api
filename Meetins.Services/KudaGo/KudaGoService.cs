using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Models.KudaGo;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meetins.Services.KudaGo
{
    /// <summary>
    /// Получение информации из сервиса KudaGo.
    /// </summary>
    public class KudaGoService : IKudaGoService
    {
        private IKudaGoRepository _kudaGoRepository;

        public KudaGoService(IKudaGoRepository kudaGoRepository)
        {
            _kudaGoRepository = kudaGoRepository;
        }

        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список всех доступных городов. </returns>
        public async Task<IEnumerable<KudaGoOutput>> GetAllAvailableCitiesAsync()
        {
            return await _kudaGoRepository.GetAllAvailableCitiesAsync();
        }
    }
}
