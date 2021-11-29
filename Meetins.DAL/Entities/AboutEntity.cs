using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Entities
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
