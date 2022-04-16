using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей Messenger.DialogMembers
    /// </summary>
    [Table("DialogMembers", Schema = "Messenger")]
    public class DialogMembersEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [Column("Id", TypeName = "uuid")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор диалога.
        /// </summary>
        [ForeignKey("DialogMembers_DialogId_fkey")]
        [Column("DialogId", TypeName = "uuid")]
        public Guid DialogId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [ForeignKey("DialogMembers_UserId_fkey")]
        [Column("UserId", TypeName = "uuid")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [Column("Status", TypeName = "varchar")]
        public String Status { get; set; }

        /// <summary>
        /// DialogEntity для внешнего ключа
        /// </summary>
        public DialogEntity Dialog { get; set; }

        /// <summary>
        /// UserEntity для внешнего ключа
        /// </summary>
        public UserEntity User { get; set; }
    }
}
