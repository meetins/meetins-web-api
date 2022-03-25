using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Models.KudaGo;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meetins.Services.KudaGo
{
    /// <summary>
    /// В реппозитории содержится функционал для получения списка всех доступных городов.
    /// </summary>
    public class KudaGoRepository : IKudaGoRepository
    {
        private PostgreDbContext _context;
        private string url = "https://kudago.com/public-api/v";

        public KudaGoRepository(PostgreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список доступных городов. </returns>
        public async Task<IEnumerable<KudaGoOutput>> GetAllAvailableCitiesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url + "1.4/locations/?lang=ru");


                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<IEnumerable<KudaGoOutput>>();
                    }

                    return null;

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
