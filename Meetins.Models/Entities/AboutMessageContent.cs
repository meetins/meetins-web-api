using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("MessageContents", Schema = "Messenger")]
    public class AboutMessageContent
    {
        [Key]
        [Column("MessageContentId")]
        public Guid MessageContentId { get; set; }

        [Column("MessageId")]
        public Guid MessageId { get; set; }

        [Column("Type")]
        public string Type { get; set; }

        [Column("Content")]
        public string Content { get; set; }
    }
}
