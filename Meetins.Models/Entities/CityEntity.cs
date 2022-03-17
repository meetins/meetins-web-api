using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей dbo.Cities.
    /// </summary>
    [Table("Cities", Schema = "dbo")]
    public class CityEntity
    {
        /// <summary>
        /// Идентификатор города.
        /// </summary>
        [Key]
        [Column("CityId", TypeName = "uuid")]
        public Guid CityId { get; set; }

        /// <summary>
        /// Название города.
        /// </summary>
        [Column("CityName", TypeName = "varchar")]
        public string CityName { get; set; }
    }
}
