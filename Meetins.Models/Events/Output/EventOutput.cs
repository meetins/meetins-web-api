using System;
using System.Collections.Generic;

namespace Meetins.Models.Events.Output
{
    public class EventOutput
    {
        /// <summary>
        /// Идентификатор события.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Категория события.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>       
        public string Title { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>       
        public string Description { get; set; }

        /// <summary>
        /// Путь к главной фотографии события (постеру).
        /// </summary>        
        public string MainPoster { get; set; }

        /// <summary>
        /// Возможна ли подписка на событие.
        /// </summary>
        public bool IsSubscriptionPossible { get; set; }

        /// <summary>
        /// Возможно ли приглашение на событие.
        /// </summary>
        public bool IsInvitePosiible { get; set; }

        /// <summary>
        /// Количество подписчиков на событие.
        /// </summary>
        public int SubscribersCount { get; set; }

        /// <summary>
        /// Подписчики.
        /// </summary>
        public List<Subscriber> Subscribers { get; set; }

    }

    /// <summary>
    /// Класс подписчика на событие.
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Аватар.
        /// </summary>
        public string Avatar { get; set; }
    }
}
