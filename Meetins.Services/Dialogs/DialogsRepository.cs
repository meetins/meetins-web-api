using Meetins.Abstractions.Services;
using Meetins.Communication;
using Meetins.Core.Data;
using Meetins.Models.Entities;
using Meetins.Models.Messages;
using Meetins.Models.User.Output;
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
                                where x.DialogMembers.Any(d => d.User.UserId.Equals(userId))
                                select new DialogsOutput
                                {
                                    DialogId = x.DialogId,
                                    Content = x.Messages.OrderByDescending(e => e.SendAt).Take(1).First().MessageContent.Content
                                })
                                .ToListAsync();

            foreach (var item in result)
            {
                item.UserName = await GetName(item.DialogId, userId);
                item.UserAvatar = await GetAvatar(item.DialogId, userId);
            }

            return result;
        }

        private async Task<string> GetName(Guid dialogId, Guid myId)
        {
            var name = await _context.DialogMembers.Where(d => d.DialogId == dialogId && d.UserId != myId).Include(u => u.User).FirstOrDefaultAsync();

            return name.User.Name;
        }

        private async Task<string> GetAvatar(Guid dialogId, Guid myId)
        {
            var avatar = await _context.DialogMembers.Where(d => d.DialogId == dialogId && d.UserId != myId).Include(user => user.User).FirstOrDefaultAsync();

            return avatar.User.Avatar;
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        public async Task<IEnumerable<MessagesOutput>> GetMessagesOfDialogAsync(Guid dialogId)
        {
            var messages = await _context.Messages.Where(d => d.DialogId.Equals(dialogId))
                             .Include(d => d.Dialog)
                             .Include(d => d.MessageContent)
                             .Include(d => d.User)
                             .OrderBy(d => d.SendAt)
                             .Select(d => new MessagesOutput
                             {
                                 DialogId = d.DialogId,
                                 MessageId = d.MessageId,
                                 Content = d.MessageContent.Content,
                                 SendAt = d.SendAt,
                                 SenderId = d.UserId,
                                 SenderName = d.User.Name,
                                 Avatar = d.User.Avatar
                             })
                             .ToListAsync();

            return messages;
        }

        public async Task<IEnumerable<MessagesOutput>> SendMessageAsync(Guid dialogId, Guid senderId, string content)
        {
            try
            {
                if (String.IsNullOrEmpty(dialogId.ToString()))
                {
                    throw new ArgumentNullException($"{dialogId} is null or empty!");
                }

                MessageEntity newMessage = new()
                {
                    MessageId = Guid.NewGuid(),
                    DialogId = dialogId,
                    UserId = senderId,
                    SendAt = DateTime.Now
                };
                await _context.AddAsync(newMessage);
                await _context.SaveChangesAsync();

                MessageContentsEntity messageContents = new()
                {
                    MessageContentId = Guid.NewGuid(),
                    MessageId = newMessage.MessageId,
                    Content = content,
                    Type = "text"
                };
                await _context.AddAsync(messageContents);

                await _context.SaveChangesAsync();

                var messages = await _context.Messages.Where(d => d.DialogId.Equals(dialogId))
                            .Include(d => d.Dialog)
                            .Include(d => d.MessageContent)
                            .Include(d => d.User)
                            .OrderBy(d => d.SendAt)
                            .Select(d => new MessagesOutput
                            {
                                DialogId = d.DialogId,
                                MessageId = d.MessageId,
                                Content = d.MessageContent.Content,
                                SendAt = d.SendAt,
                                SenderId = d.UserId,
                                SenderName = d.User.Name,
                                Avatar = d.User.Avatar
                            })
                            .ToListAsync();

                return messages;
            }
            catch (Exception)
            {
                //логируем
                throw;
            }
        }

        public async Task<IEnumerable<MessagesOutput>> StartDialogAsync(Guid senderId, Guid userId, string content)
        {
            try
            {
                var findedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

                if (findedUser is null)
                {
                    throw new ArgumentNullException($"С {userId} нельзя начать диалог - такого пользователя не существует!");
                }

                //если уже диалог существует
                var findedDialog = await GetPrivateDialogAsync(senderId, userId);

                if (findedDialog is not null)
                {
                    return await SendMessageAsync(findedDialog.DialogId, senderId, content);
                }

                DateTime dateTime = DateTime.Now;

                DialogEntity createdDialog = new()
                {
                    DialogId = Guid.NewGuid(),
                    CreatedAt = dateTime
                };

                await _context.AddAsync(createdDialog);
                await _context.SaveChangesAsync();
                DialogMembersEntity dialogSender = new()
                {
                    DialogId = createdDialog.DialogId,
                    UserId = senderId
                };
                DialogMembersEntity dialogMember = new()
                {
                    DialogId = createdDialog.DialogId,
                    UserId = userId
                };

                await _context.AddAsync(dialogSender);
                await _context.AddAsync(dialogMember);
                await _context.SaveChangesAsync();
                MessageEntity newMessage = new()
                {
                    MessageId = Guid.NewGuid(),
                    DialogId = createdDialog.DialogId,
                    UserId = senderId,
                    SendAt = dateTime
                };
                await _context.AddAsync(newMessage);
                await _context.SaveChangesAsync();
                MessageContentsEntity messageContents = new()
                {
                    MessageContentId = Guid.NewGuid(),
                    MessageId = newMessage.MessageId,
                    Content = content,
                    Type = "text"
                };
                await _context.AddAsync(messageContents);

                await _context.SaveChangesAsync();

                var messages = await _context.Messages.Where(d => d.DialogId.Equals(createdDialog.DialogId))
                            .Include(d => d.Dialog)
                            .Include(d => d.MessageContent)
                            .Include(d => d.User)
                            .OrderBy(d => d.SendAt)
                            .Select(d => new MessagesOutput
                            {
                                DialogId = d.DialogId,
                                MessageId = d.MessageId,
                                Content = d.MessageContent.Content,
                                SendAt = d.SendAt,
                                SenderId = d.UserId,
                                SenderName = d.User.Name,
                                Avatar = d.User.Avatar
                            })
                            .ToListAsync();

                return messages;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserOutput>> GetOtherDialogMembersAsync(Guid dialogId, Guid userId)
        {
            try
            {
                var result = await _context.DialogMembers
                    .Where(u => u.DialogId == dialogId && u.UserId != userId)
                    .Include(u=>u.User)
                    .Select(u => new UserOutput
                    {
                        UserId = u.UserId,
                        Name = u.User.Name,
                        Email = u.User.Email,
                        Gender = u.User.Gender,
                        Login = u.User.Login,
                        DateRegister = u.User.DateRegister,
                        Status = u.Status,
                        Avatar = u.User.Avatar,
                        BirthDate = u.User.BirthDate,
                        PhoneNumber = u.User.PhoneNumber
                    }).ToListAsync();

                return result;
            }
            catch (Exception)
            {
                //TODO: Logging here
                throw;
            }
        }

        /// <summary>
        /// Полное удаление диалогов и сообщений пользователя.
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns></returns>
        public async Task<Task> DeleteAllUserDialogsAsync(Guid userId)
        {
            var dialogsToDelete = await _context.Dialogs.Include(d => d.DialogMembers)
                .Where(d => d.DialogMembers.Any(d => d.User.UserId.Equals(userId)))
                .ToListAsync();

            _context.Dialogs.RemoveRange(dialogsToDelete);

            await _context.SaveChangesAsync();

            return Task.CompletedTask;
        }
    
        public async Task<DialogMembersEntity> GetPrivateDialogAsync(Guid userId, Guid otherUserId)
        {
            try
            {
                var userDialogs = await _context.DialogMembers                   
                    .Where(d => d.UserId.Equals(userId))
                    .ToListAsync();

                var otherUserDialog = await _context.DialogMembers
                    .Where(d => d.UserId.Equals(otherUserId))
                    .ToListAsync();

                foreach (var item in userDialogs)
                {
                    var result = otherUserDialog.FirstOrDefault(f=>f.DialogId == item.DialogId);

                    if (result is not null)
                    {
                        return result;
                    }
                }

                return null;
              
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
