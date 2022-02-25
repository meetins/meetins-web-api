using Meetins.Abstractions.Services;
using Meetins.Communication;
using Meetins.Core.Data;
using Meetins.Models.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.Dialogs
{
    /// <summary>
    /// В реппозитории содержится функциоанл для получения всех диалогов авторизованного пользователя и получения всех сообщений, принадлежащих диалогу из базы данных.
    /// </summary>
    public class DialogsRepository : IDialogsRepository
    {
        private PostgreDbContext _context;

        public DialogsRepository(PostgreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получения всех диалогов авторизованного пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns> Список всех диалогов пользователя. </returns>
        public async Task<IEnumerable<DialogsOutput>> GetDialogsAsync(Guid userId)
        {
            var result = await (from x in _context.Dialogs
                                .Include(dialog => dialog.DialogMembers)
                                    .ThenInclude(dialogMember => dialogMember.User)
                                .Include(dialog => dialog.Messages)
                                    .ThenInclude(message => message.MessageContent)
                                .Include(dialog => dialog.Messages)
                                    .ThenInclude(message => message.ChatMessage)
                                where x.DialogMembers.Any(d => d.User.UserId.Equals(userId))
                                select new DialogsOutput
                                {
                                    DialogId = x.DialogId,
                                    UserName = x.DialogMembers.First().User.Name,
                                    UserAvatar = x.DialogMembers.First().User.Avatar,
                                    IsRead = x.Messages.OrderByDescending(e => e.SendAt).Take(1).First().ChatMessage.IsRead,
                                    Content = x.Messages.OrderByDescending(e => e.SendAt).Take(1).First().MessageContent.Content
                                })
                                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        public async Task<IEnumerable<MessagesOutput>> GetMessagesOfDialogAsync(Guid dialogId)
        {
            var result = await (from x in _context.Messages
                                .Include(m => m.Dialog)
                                .Include(m => m.MessageContent)
                                .Include(m => m.ChatMessage)
                                .Include(m => m.User)
                                where x.DialogId.Equals(dialogId)
                                select new MessagesOutput 
                                { 
                                    MessageId = x.MessageId,
                                    Content = x.MessageContent.Content,
                                    SendAt = x.SendAt,
                                    SenderId = x.SenderId,
                                    IsRead = x.ChatMessage.IsRead,
                                    Avatar = x.User.Avatar
                                })
                                .ToListAsync();
            return result;
        }
    }
}
