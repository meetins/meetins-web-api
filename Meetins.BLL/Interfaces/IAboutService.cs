using Meetins.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<AboutDto>> GetAboutsAsync();
    }
}
