using Meetins.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IHeaderService
    {
        Task<IEnumerable<HeaderDto>> GetAllUsersAsync();
    }
}
