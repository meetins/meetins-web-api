using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("DialogMembers", Schema = "Messenger")]
    public class DialogMembers
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("DialogId")]
        public Guid DialogId { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("Status")]
        public String Status { get; set; }
    }
}
