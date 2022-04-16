using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей User.RefreshTokens.
    /// </summary>
    [Table("RefreshTokens", Schema = "User")]
    public class RefreshTokenEntity
    {
        /// <summary>
        /// Идентификатор обновления токена.
        /// </summary>
        [Key]
        [Column("RefreshTokenId")]
        public Guid RefreshTokenId { get; set; }

        /// <summary>
        /// Токен.
        /// </summary>
        [Column("RefreshToken")]
        public string Token { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [Column("UserId")]
        public Guid UserId { get; set; }
    }
}
