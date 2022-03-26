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
    /// В репозитории содержится функционал для получения информации из сервиса KudaGo.
    /// </summary>
    public class KudaGoRepository : IKudaGoRepository
    {
        private string url = "https://kudago.com/public-api/v1.4/locations/?lang=ru";

        public KudaGoRepository()
        {
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
                    var response = await client.GetAsync(url);


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
