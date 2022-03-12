using Meetins.Abstractions.Services;
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
        //[Authorize]
        [HttpGet, Route("all")]
        public async Task<ActionResult<IEnumerable<PeopleOutput>>> GetAllPeoplesAsync()
        {
            //string rawUserId = HttpContext.User.FindFirst("userId").Value;

            string rawUserId = "5db1031e-ca48-46d1-b9ea-d9e7ebb8c6e6";

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _peopleService.GetAllPeoplesAsync(userId);

            return Ok(result);
        }
    }
}
