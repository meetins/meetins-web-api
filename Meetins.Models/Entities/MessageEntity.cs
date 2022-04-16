using System;
using System.Collections.Generic;
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
        [ForeignKey("Messages_DialogId_fkey")]
        [Column("DialogId", TypeName = "uuid")]
        public Guid DialogId { get; set; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        [ForeignKey("User")]
        [Column("UserId", TypeName = "uuid")]
        public Guid SenderId { get; set; }

        /// <summary>
        /// Дата и время отправления.
        /// </summary>
        [Column("SendAt", TypeName = "timestamp")]
        public DateTime SendAt { get; set; }

        /// <summary>
        /// Прочитано или нет.
        /// </summary>
        [Column("IsRead", TypeName = "bool")]
        public bool IsRead { get; set; }

        public MessageEntity Message { get; set; }

        /// <summary>
        /// DialogEntity для внешнего ключа
        /// </summary>
        public DialogEntity Dialog { get; set; }

        /// <summary>
        /// MessageContentsEntity для внешнего ключа
        /// </summary>
        public MessageContentsEntity MessageContent { get; set; }

        /// <summary>
        /// UserEntity для внешнего ключа
        /// </summary>
        public UserEntity User { get; set; }
    }
}
