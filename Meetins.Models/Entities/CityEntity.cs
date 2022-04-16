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

        /// <summary>
        /// Нормализованное имя.
        /// </summary>
        [Column("NormalizedName", TypeName ="varchar")]
        public string NormalizedName { get; set; }

        /// <summary>
        /// Слаг.
        /// </summary>
        [Column("Slug", TypeName = "varchar")]
        public string Slug { get; set; }

        /// <summary>
        /// Имеет ли события на Kudago.
        /// </summary>
        [Column("HasKudagoEvents", TypeName = "boolean")]
        public bool HasKudagoEvents { get; set; }
    }
}
