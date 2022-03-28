using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.KudaGo;
using System;
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
        private PostgreDbContext _postgreDbContext;

        public KudaGoService(IKudaGoRepository kudaGoRepository, PostgreDbContext postgreDbContext)
        {
            _kudaGoRepository = kudaGoRepository;
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список всех доступных городов. </returns>
        public async Task<IEnumerable<KudaGoOutput>> GetAllAvailableCitiesAsync()
        {
            try
            {
                return await _kudaGoRepository.GetAllAvailableCitiesAsync();
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
