using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Communication
{
    /// <summary>
    /// Выходная модель для диалогов.
    /// </summary>
    public class DialogsOutput
    {
        /// <summary>
        /// Идентификатор диалога.
        /// </summary>
        public Guid DialogId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Аватар пользователя.
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// Прочитано последнее ли сообщение.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Содержание последнего сообщения.
        /// </summary>
        public string Content { get; set; }
    }
}
