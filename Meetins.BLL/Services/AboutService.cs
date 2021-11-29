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
    public class AboutService : IAboutService
    {
        private IUnitOfWork _db;

        public AboutService(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        public async Task<IEnumerable<AboutDto>> GetAboutsAsync()
        {
            var abouts = await _db.Abouts.GetAboutsAsync();

            List<AboutDto> aboutDtos = new List<AboutDto>();

            foreach (var item in abouts)
            {
                aboutDtos.Add(new AboutDto
                {
                    MainText = item.MainText,
                    Description = item.Description
                });
            }

            return aboutDtos;
        }
    }
}
