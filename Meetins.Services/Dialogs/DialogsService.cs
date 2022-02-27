using Meetins.Abstractions.Services;
using Meetins.Communication;
using Meetins.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.Services.Dialogs
{
    /// <summary>
    /// Получение всех диалогов авторизованного пользователя и получения всех сообщений, принадлежащих диалогу
    /// </summary>
    public class DialogsService : IDialogsService
    {
        private IDialogsRepository _dialogsRepository;

        public DialogsService(IDialogsRepository dialogsRepository)
        {
            _dialogsRepository = dialogsRepository;
        }

        /// <summary>
        /// Получения всех диалогов авторизованного пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns> Список всех диалогов пользователя. </returns>
        public async Task<IEnumerable<DialogsOutput>> GetDialogsAsync(Guid userId)
        {
            var result = await _dialogsRepository.GetDialogsAsync(userId);

            return result;
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        public async Task<IEnumerable<MessagesOutput>> GetMessagesOfDialog(Guid dialogId)
        {
            var result = await _dialogsRepository.GetMessagesOfDialogAsync(dialogId);

            return result;
        }
    }
}
