using Meetins.Abstractions.Services;
using Meetins.Communication;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Dialogs.Output;
using Meetins.Models.Entities;
using Meetins.Models.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Services.Dialogs
{
    /// <summary>
    /// Получение всех диалогов авторизованного пользователя и получения всех сообщений, принадлежащих диалогу
    /// </summary>
    public class DialogsService : IDialogsService
    {
        private IDialogsRepository _dialogsRepository;
        private PostgreDbContext _postgreDbContext;

        public DialogsService(IDialogsRepository dialogsRepository, PostgreDbContext postgreDbContext)
        {
            _dialogsRepository = dialogsRepository;
            _postgreDbContext = postgreDbContext;
        }

        /// <summary>
        /// Получения всех диалогов авторизованного пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns> Список всех диалогов пользователя. </returns>
        public async Task<IEnumerable<DialogsOutput>> GetDialogsAsync(Guid userId)
        {
            try
            {
                var result = await _dialogsRepository.GetDialogsAsync(userId);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        public async Task<IEnumerable<MessagesOutput>> GetMessagesOfDialog(Guid dialogId)
        {
            try
            {
                var result = await _dialogsRepository.GetMessagesOfDialogAsync(dialogId);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        public async Task<IEnumerable<UserEntity>> GetOtherDialogMembersAsync(Guid dialogId, Guid userId)
        {
            try
            {
                var result = await _dialogsRepository.GetOtherDialogMembersAsync(dialogId, userId);

                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        public async Task<IEnumerable<MessagesOutput>> SendMessageAsync(Guid dialogId, Guid senderId, string content)
        {
            try
            {
                var result = await _dialogsRepository.SendMessageAsync(dialogId, senderId, content);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        public async Task<IEnumerable<MessagesOutput>> StartDialogAsync(Guid senderId, Guid userId, string content)
        {
            try
            {
                var result = await _dialogsRepository.StartDialogAsync(senderId, userId, content);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// <summary>
        /// Полное удаление диалогов и сообщений пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns></returns>
        public async Task<bool> DeleteAllUserDialogsAndMessagesAsync(Guid userId)
        {
            try
            {
                return await _dialogsRepository.DeleteAllUserDialogsAndMessagesAsync(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }

        /// Метод вернет информацию о диалоге, если он существует.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="otherUserId">Идентификатор пользователя, с которым ищется диалог.</param>
        /// <returns>Выходная модель свойств диалога.</returns>
        public async Task<DialogPropretiesOutput> GetPrivateDialogAsync(Guid userId, Guid otherUserId)
        {
            try
            {
                var result = await _dialogsRepository.GetPrivateDialogAsync(userId, otherUserId);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }
        }
    }
}
