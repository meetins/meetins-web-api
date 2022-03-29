using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Core.Exceptions;
using Meetins.Models.KudaGo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Meetins.Services.KudaGo
{
    /// <summary>
    /// В репозитории содержится функционал для получения информации из сервиса KudaGo.
    /// </summary>
    public class KudaGoRepository : IKudaGoRepository
    {
        private string ApiUrl = "https://kudago.com/public-api/";
        private string ApiVersion = "v1.4";

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

                var response = await client.GetAsync(ApiUrl + ApiVersion + "/locations/?lang=ru");


                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<KudaGoOutput>>();
                }
                else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    throw new NotFoundException("KudaGo Api locations notfound result code");
                }
                else
                {
                    throw new Exception("KudaGo Api locations " + response.StatusCode.ToString() + " result code");
                }
            }
        }
    }
}
