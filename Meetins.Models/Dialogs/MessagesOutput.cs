using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Models.Messages
{
    /// <summary>
    /// Выходная модель для сообщений.
    /// </summary>
    public class MessagesOutput
    {
        public Guid DialogId { get; set; }

        /// <summary>
        /// Содержание сообщения.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Дата и время отправления.
        /// </summary>
        public DateTime SendAt { get; set; }

        /// <summary>
        /// Отправитель сообщения.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Прочитано ли сообщение.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Аватарка.
        /// </summary>
        public string Avatar { get; set; }

        public string SenderName { get; set; }

        public bool IsMine { get; set; }
    }
}
