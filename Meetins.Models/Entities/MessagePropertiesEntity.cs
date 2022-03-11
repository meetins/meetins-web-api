using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс  сопоставляется с таблицей Messenger.MessageProperties
    /// </summary>
    [Table("MessageProperties", Schema = "Messenger")]
    public class MessagePropertiesEntity
    {
        /// <summary>
        /// Идентификатор свойств сообщения.
        /// </summary>
        [Key]
        [Column("MessagePropertiesId", TypeName = "uuid")]
        public Guid MessagePropertiesId { get; set; }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        [ForeignKey("MessageProperties_MessageId_fkey")]
        [Column("MessageId", TypeName = "uuid")]
        public Guid MessageId { get; set; }
        
        /// <summary>
        /// Прочитано или нет.
        /// </summary>
        [Column("IsRead", TypeName = "bool")]
        public bool IsRead { get; set; }

        public MessageEntity Message { get; set; }
    }
}
