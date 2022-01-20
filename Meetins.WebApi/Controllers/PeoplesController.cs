using Meetins.BLL.DTOs;
using Meetins.BLL.Interfaces;
using Meetins.WebApi.Models.Peoples.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.WebApi.Controllers
{
    [Route("peoples")]
    [ApiController]
    public class PeoplesController : Controller
    {
        private IUserService _userService;
        
        public PeoplesController(IUserService userService)
        {
            _userService = userService;            
        }

        [Authorize]
        [HttpGet, Route("get-all")]
        public async Task<ActionResult<IEnumerable<PeoplesResponseModel>>> CheckLoginurl()
        {
            List<PeoplesResponseModel> peoples = new List<PeoplesResponseModel>();

            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();
            
            foreach (var user in users.ToList())
            {
                peoples.Add(new PeoplesResponseModel { 
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AvatarPath = user.AvatarPath,
                    LoginUrl = user.LoginUrl
                });
            }

            return Ok(peoples);

        }
    }
}
