using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("Messages", Schema = "Messenger")]
    public class MessageEntity
    {
        [Key]
        [Column("MessageId")]
        public Guid MessageId { get; set; }

        [Column("DialogId")]
        public Guid DialogId { get; set; }

        [Column("SenderId")]
        public Guid SenderId { get; set; }

        [Column("SendAt")]
        public DateTime SendAt { get; set; }
    }
}
