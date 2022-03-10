﻿using Meetins.Abstractions.Services;
using Meetins.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Controllers
{
    /// <summary>
    /// В контроллере содержится функционал для получения списка всех пользователей.
    /// </summary>
    [Route("people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;
        
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;            
        }

        /// <summary>
        /// Получение списка всех существующих пользователей.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        [Authorize]
        [HttpGet, Route("all")]
        public async Task<ActionResult<IEnumerable<PeopleOutput>>> GetAllPeoplesAsync()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            //string rawUserId = "187ac176-cb28-4456-9ab5-d3a1ef370500";

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _peopleService.GetAllPeoplesAsync(userId);

            return Ok(result);
        }
    }
}
