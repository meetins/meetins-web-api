using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей Messenger.MessageContents
    /// </summary>
    [Table("MessageContents", Schema = "Messenger")]
    public class MessageContentsEntity
    {
        /// <summary>
        /// Идентификатор содержания сообщения.
        /// </summary>
        [Key]
        [Column("MessageContentId", TypeName = "uuid")]
        public Guid MessageContentId { get; set; }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        [ForeignKey("MessageContents_MessageId_fkey")]
        [Column("MessageId", TypeName = "uuid")]
        public Guid MessageId { get; set; }

        /// <summary>
        /// Тип сообщения.
        /// </summary>
        [Column("Type", TypeName = "varchar")]
        public string Type { get; set; }

        /// <summary>
        /// Содержание сообщения.
        /// </summary>
        [Column("Content", TypeName = "varchar")]
        public string Content { get; set; }

        /// <summary>
        /// MessageEntity для внешнего ключа
        /// </summary>
        public MessageEntity Message { get; set; }
    }
}
