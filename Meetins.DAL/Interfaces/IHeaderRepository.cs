using Meetins.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Interfaces
{
    public interface IHeaderRepository
    {
        Task<IEnumerable<HeaderEntity>> GetAllHeadersAsync();
    }
}
