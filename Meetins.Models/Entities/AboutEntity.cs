using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей Info.About.
    /// </summary>
    [Table("About", Schema = "Info")]
    public class AboutEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [Column("AboutId")]
        public Guid AboutId { get; set; }

        /// <summary>
        /// Главный текст.
        /// </summary>
        [Column("MainText")]
        public string MainText { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("Description")]
        public string Description { get; set; }
    }
}
