using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей Messenger.Messages
    /// </summary>
    [Table("Messages", Schema = "Messenger")]
    public class MessageEntity
    {
        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        [Key]
        [Column("MessageId", TypeName = "uuid")]
        public Guid MessageId { get; set; }

        /// <summary>
        /// Идентификатор диалога.
        /// </summary>
        [Column("DialogId", TypeName = "uuid")]
        public Guid DialogId { get; set; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        [Column("SenderId", TypeName = "uuid")]
        public Guid SenderId { get; set; }

        /// <summary>
        /// Дата и время отправления.
        /// </summary>
        [Column("SendAt", TypeName = "timestamp")]
        public DateTime SendAt { get; set; }
    }
}
