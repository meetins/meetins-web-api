using Meetins.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<string> GenerateTokenAsync(string email, string password);
        Task<UserDto> RegisterUserAsync(UserDto user);
    }
}
