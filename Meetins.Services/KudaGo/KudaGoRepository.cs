using Meetins.Abstractions.Repositories;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Exceptions;
using Meetins.Models.KudaGo;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meetins.Services.KudaGo
{
    /// <summary>
    /// В репозитории содержится функционал для получения информации из сервиса KudaGo.
    /// </summary>
    public class KudaGoRepository : IKudaGoRepository
    {
        private string ApiUrl = "https://kudago.com/public-api/";
        private string ApiVersion = "v1.4";
        private readonly PostgreDbContext _postgreDbContext;

        public KudaGoRepository(PostgreDbContext postgreDbContext)
        {
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Получение списка всех доступных городов.
        /// </summary>
        /// <returns> Список доступных городов. </returns>
        public async Task<IEnumerable<KudaGoCitiesOutput>> GetAllAvailableCitiesAsync()
        {
            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync(ApiUrl + ApiVersion + "/locations/?lang=ru");


                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<KudaGoCitiesOutput>>();
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

        /// <summary>
        /// Получение списка всех категорий событий.
        /// </summary>
        /// <returns> Список категорий событий. </returns>
        public async Task<IEnumerable<KudaGoCategoriesOutput>> GetAllEventСategoriesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrl + ApiVersion + "/event-categories/?lang=ru");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<KudaGoCategoriesOutput>>();
                }
                else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    throw new NotFoundException("KudaGo Api event categories notfound result code");
                }
                else
                {
                    throw new Exception("KudaGo Api categories " + response.StatusCode.ToString() + " result code");
                }
            }
        }

        /// <summary>
        /// Получение списка всех категорий мест.
        /// </summary>
        /// <returns> Список категорий мест. </returns>
        public async Task<IEnumerable<KudaGoCategoriesOutput>> GetAllPlaceСategoriesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrl + ApiVersion + "/place-categories/?lang=ru");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<KudaGoCategoriesOutput>>();
                }
                else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    throw new NotFoundException(("KudaGo Api place categories notfound result code"));
                }
                else
                {
                    throw new Exception("KudaGo Api place categories " + response.StatusCode.ToString() + " result code");
                }
            }
        }

        /// <summary>
        /// Получение списка всех доступных мест.
        /// </summary>
        /// <returns> Список всех доступных мест. </returns>
        public async Task<KudaGoPlacesOutput> GetAllAvailablePlacesAsync(int numberOfPage)
        {
            using (HttpClient client = new HttpClient())
            {
                var responseMessage = await client.GetAsync(ApiUrl + ApiVersion + "/places/?page=" + numberOfPage);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<KudaGoPlacesOutput>(responseJson);
                    response.Page = numberOfPage;
                    return response;
                }
                else if (responseMessage.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    throw new NotFoundException(("KudaGo Api places notfound result code"));
                }
                else
                {
                    throw new Exception("KudaGo Api places " + responseMessage.StatusCode.ToString() + " result code");
                }
            }
        }

        /// <summary>
        /// Метод вернёт все приглашения для пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список приглашений для пользователя.</returns>
        public async Task<IEnumerable<KudagoInvitesOutput>> GetMyInvitesAsync(Guid userId)
        {
            try
            {
                var result = await _postgreDbContext.KudagoInvites
                                    .Where(u => u.UserIdTo == userId)
                                    .Include(u=>u.UserFrom)
                                    .Select(u=> new KudagoInvitesOutput
                                    {
                                        InviteId = u.InviteId,
                                        KudagoEventId = u.KudagoEventId,
                                        Comment = u.Comment,
                                        CreatedAt = u.CreatedAt,
                                        UserIdFrom = u.UserIdFrom,
                                        UserNameFrom = u.UserFrom.Name,
                                        UserAvatarFrom = u.UserFrom.Avatar,
                                        IsViewed = u.IsViewed
                                    })
                                    .ToListAsync();
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
