using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей Messenger.Dialogs
    /// </summary>
    [Table("Dialogs", Schema = "Messenger")]
    public class DialogEntity
    {
        /// <summary>
        /// Идентификатор диалога.
        /// </summary>
        [Key]
        [Column("DialogId", TypeName = "uuid")]
        public Guid DialogId { get; set; }

        /// <summary>
        /// Дата и время создания диалога.
        /// </summary>
        [Column("CreatedAt", TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }
    }
}
