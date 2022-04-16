﻿using Meetins.Abstractions.Repositories;
using Meetins.Models.Exceptions;
using Meetins.Models.KudaGo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public KudaGoRepository()
        {
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
    }
}
