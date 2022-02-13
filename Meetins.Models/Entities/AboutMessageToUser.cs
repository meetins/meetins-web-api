using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("MessageToUser", Schema = "Messenger")]
    public class AboutMessageToUser
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("MessageId")]
        public Guid MessageId { get; set; }

        [Column("RecipientId")]
        public Guid RecipientId { get; set; }

        [Column("IsRead")]
        public bool IsRead { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
