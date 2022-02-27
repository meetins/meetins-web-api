using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс  сопоставляется с таблицей Messenger.ChatMessage
    /// </summary>
    [Table("ChatMessage", Schema = "Messenger")]
    public class ChatMessageEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [Column("Id", TypeName = "uuid")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        [ForeignKey("chatmessage_fk")]
        [Column("MessageId", TypeName = "uuid")]
        public Guid MessageId { get; set; }
        
        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        [ForeignKey("chatmessage1_fk")]
        [Column("UserId", TypeName = "uuid")]
        public Guid RecipientId { get; set; }

        /// <summary>
        /// Прочитано или нет.
        /// </summary>
        [Column("IsRead", TypeName = "bool")]
        public bool IsRead { get; set; }

        /// <summary>
        /// Удалено или нет.
        /// </summary>
        [Column("IsDeleted", TypeName = "bool")]
        public bool IsDeleted { get; set; }

        public MessageEntity Message { get; set; }
    }
}
