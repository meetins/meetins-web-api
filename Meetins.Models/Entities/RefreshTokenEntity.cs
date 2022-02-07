using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Models.Entities
{
    [Table("RefreshTokens", Schema = "User")]
    public class RefreshTokenEntity
    {
        [Key]
        [Column("RefreshTokenId")]
        public Guid RefreshTokenId { get; set; }

        [Column("Token")]
        public string Token { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }
    }
}
