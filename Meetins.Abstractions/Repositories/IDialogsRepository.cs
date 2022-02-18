using Meetins.Communication;
using Meetins.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция репозитория диалогов.
    /// </summary>
    public interface IDialogsRepository
    {
        /// <summary>
        /// Получения всех диалогов авторизованного пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns> Список всех диалогов пользователя. </returns>
        Task<IEnumerable<DialogsOutput>> GetDialogsAsync(Guid userId);

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        Task<IEnumerable<MessagesOutput>> GetMessagesOfDialog(Guid dialogId);
    }
}
