using Meetins.Abstractions.Services;
using Meetins.Communication;
using Meetins.Core.Data;
using Meetins.Models.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result = await (from a in _context.DialogMembers
                                where a.UserId.Equals(userId)
                                select new DialogsOutput
                                {
                                    Id = a.Id,
                                    DialogId = a.DialogId,
                                    UserId = a.UserId,
                                    Status = a.Status
                                })
                    .ToListAsync();

            return result;
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        public async Task<IEnumerable<MessagesOutput>> GetMessagesOfDialog(Guid dialogId)
        {
            var result = await (from a in _context.Messages
                                where a.DialogId.Equals(dialogId)
                                join b in _context.MessageContents on
                                a.MessageId equals b.MessageId
                                select new MessagesOutput
                                {
                                    Content = b.Content
                                })
                .ToListAsync();

            return result;
        }
    }
}
