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
        IEnumerable<UserDto> GetAllUsers();

        void Dispose();
    }
}
