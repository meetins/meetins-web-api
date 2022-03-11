using Meetins.Communication;
using Meetins.Models.Messages;
using Meetins.Models.User.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Abstractions.Services
{
    /// <summary>
    /// Абстракция сервиса диалогов.
    /// </summary>
    public interface IDialogsService
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

        Task<IEnumerable<MessagesOutput>> SendMessageAsync(Guid dialogId, Guid senderId, string content);

        Task<IEnumerable<MessagesOutput>> StartDialogAsync(Guid senderId, Guid userId, string content);

        Task<IEnumerable<UserOutput>> GetOtherDialogMembersAsync(Guid dialogId, Guid userId);

        Task<Task> DeleteAllUserDialogsAsync(Guid userId);
    }
}
