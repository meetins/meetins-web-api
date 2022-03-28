using Meetins.Abstractions.Repositories;
using Meetins.Abstractions.Services;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.Common
{
    /// <summary>
    /// Сервис общего функционала.
    /// </summary>
    public class CommonService : ICommonService
    {
        private ICommonRepository _commonRepository;
        private PostgreDbContext _postgreDbContext;

        public CommonService(ICommonRepository commonRepository, PostgreDbContext postgreDbContext)
        {
            _commonRepository = commonRepository;
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Получение списка всех городов пользователей.
        /// </summary>
        /// <returns> Список все городов пользователей. </returns>
        public async Task<IEnumerable<CityOutput>> GetAllCitiesAsync()
        {
            try
            {
                var result = await _commonRepository.GetAllCitiesAsync();

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
