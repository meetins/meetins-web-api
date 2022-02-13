using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("Dialogs", Schema = "Messenger")]
    public class DialogEntity
    {
        [Key]
        [Column("DialogId")]
        public Guid DialogId { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
