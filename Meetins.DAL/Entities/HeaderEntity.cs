using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Entities
{
    [Table("Headers", Schema = "Info")]
    public class HeaderEntity
    {
        [Key]
        [Column("HeaderId")]
        public Guid HeaderId { get; set; }
        [Column("MainText")]
        public string MainText { get; set; }
        [Column("Description")]
        public string Description { get; set; }
    }
}
