using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопостовляется с таблицей "Events.Events".
    /// </summary>
    [Table("Events", Schema = "Events")]
    public class EventEntity
    {
        /// <summary>
        /// Идентификатор события, PK.
        /// </summary>
        [Key]
        [Column("EventId", TypeName = "bigserial")]
        public long EventId { get; set; }

        /// <summary>
        /// Идентификатор категории события, FK.
        /// </summary>
        [ForeignKey("Events_EventsCategoryId_fkey")]
        [Column("EventsCategoryId", TypeName = "uuid")]
        public Guid EventsCategoryId { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        [Column("Title", TypeName = "varchar(150)")]
        public string Title { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>
        [Column("Description", TypeName = "text")]
        public string Description { get; set; }

        /// <summary>
        /// Путь к главной фотографии события (постеру).
        /// </summary>
        [Column("MainPoster", TypeName = "text")]
        public string MainPoster { get; set; }

        /// <summary>
        /// Навигационное поле для категории события.
        /// </summary>
        public EventsCategoryEntity EventsCategory { get; set; }
    }
}
