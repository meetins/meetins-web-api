using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("About", Schema = "Info")]
    public class AboutEntity
    {
        [Key]
        [Column("AboutId")]
        public Guid AboutId { get; set; }
        [Column("MainText")]
        public string MainText { get; set; }
        [Column("Description")]
        public string Description { get; set; }
    }
}
