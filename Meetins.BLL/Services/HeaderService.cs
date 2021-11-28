using Meetins.BLL.DTO;
using Meetins.BLL.Interfaces;
using Meetins.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.BLL.Services
{
    public class HeaderService : IHeaderService
    {
        private IUnitOfWork _db;

        public HeaderService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        public async Task<IEnumerable<HeaderDto>> GetAllUsersAsync()
        {
            var headers = await _db.Headers.GetAllHeadersAsync();

            List<HeaderDto> headerDtos = new List<HeaderDto>();

            foreach (var item in headers)
            {
                headerDtos.Add(new HeaderDto
                {
                    MainText = item.MainText,
                    Description = item.Description
                });
            }

            return headerDtos;
        }
    }
}
