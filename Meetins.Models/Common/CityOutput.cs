using System;

namespace Meetins.Models.Common
{
    public class CityOutput
    {
        /// <summary>
        /// Идентификатор города.
        /// </summary>
        public Guid CityId { get; set; }

        /// <summary>
        /// Название города.
        /// </summary>
        public string CityName { get; set; }
    }
}
