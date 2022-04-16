using Meetins.Communication;
using Meetins.Models.Dialogs.Output;
using Meetins.Models.Entities;
using Meetins.Models.Messages;
using System;
using System.Collections.Generic;
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
        Task<IEnumerable<MessagesOutput>> GetMessagesOfDialogAsync(Guid dialogId);

        Task<IEnumerable<MessagesOutput>> SendMessageAsync(Guid dialogId, Guid senderId, string content);

        Task<IEnumerable<MessagesOutput>> StartDialogAsync(Guid senderId, Guid userId, string content);

        Task<IEnumerable<UserEntity>> GetOtherDialogMembersAsync(Guid dialogId, Guid userId);

        /// <summary>
        /// Полное удаление диалогов и сообщений пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns></returns>
        Task<bool> DeleteAllUserDialogsAndMessagesAsync(Guid userId);
        /// Метод вернет информацию о диалоге, если он существует.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="otherUserId">Идентификатор пользователя, с которым ищется диалог.</param>
        /// <returns>Выходная модель свойств диалога.</returns>
        Task<DialogPropretiesOutput> GetPrivateDialogAsync(Guid userId, Guid otherUserId);
    }
}
