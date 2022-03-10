using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей "Events.EventsToUsers".
    /// </summary>
    [Table("EventsToUsers", Schema = "Events")]
    public class EventsToUsersEntity
    {
        /// <summary>
        /// Идентификатор связи, PK.
        /// </summary>
        [Key]
        [Column("EventToUserId", TypeName = "uuid")]
        public Guid EventToUserId { get; set; }

        /// <summary>
        /// Идентификатор события, FK.
        /// </summary>
        [ForeignKey("EventsToUsers_EventId_fkey")]
        [Column("EventId", TypeName = "uuid")]
        public Guid EventId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, FK.
        /// </summary>
        [ForeignKey("EventsToUsers_UserId_fkey")]
        [Column("UserId", TypeName = "uuid")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата подписки на событие.
        /// </summary>
        [Column("SubscriptionDate", TypeName = "timestamp")]
        public DateTime LastSubscriptionDate { get; set; }

        /// <summary>
        /// Подписан ли пользователь.
        /// </summary>
        [Column("IsUserSubscribed", TypeName = "boolean")]
        public bool IsUserSubscribed { get; set; }

        /// <summary>
        /// Навигационное свойство для события.
        /// </summary>
        public EventEntity Event { get; set; }

        /// <summary>
        /// Навигационное свойство для пользователя.
        /// </summary>
        public UserEntity User { get; set; }
    }
}
