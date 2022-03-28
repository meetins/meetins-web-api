using Meetins.Abstractions.Services;
using Meetins.Communication;
using Meetins.Core.Data;
using Meetins.Core.Logger;
using Meetins.Models.Dialogs.Output;
using Meetins.Models.Entities;
using Meetins.Models.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetins.Services.Dialogs
{
    /// <summary>
    /// В реппозитории содержится функционал для получения всех диалогов авторизованного пользователя и получения всех сообщений, принадлежащих диалогу из базы данных.
    /// </summary>
    public class DialogsRepository : IDialogsRepository
    {
        private PostgreDbContext _postgreDbContext;

        public DialogsRepository(PostgreDbContext postgreDbContext)
        {
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
                var result = await (from x in _postgreDbContext.Dialogs
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                var logger = new Logger(_postgreDbContext, e.GetType().FullName, e.Message, e.StackTrace);
                await logger.LogError();
                throw;
            }            
        }

        private async Task<string> GetName(Guid dialogId, Guid myId)
        {
            var name = await _postgreDbContext.DialogMembers.Where(d => d.DialogId == dialogId && d.UserId != myId).Include(u => u.User).FirstOrDefaultAsync();

            return name.User.Name;
        }

        private async Task<string> GetAvatar(Guid dialogId, Guid myId)
        {
            var avatar = await _postgreDbContext.DialogMembers.Where(d => d.DialogId == dialogId && d.UserId != myId).Include(user => user.User).FirstOrDefaultAsync();

            return avatar.User.Avatar;
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Список всех сообщений, принадлежащих диалогу. </returns>
        public async Task<IEnumerable<MessagesOutput>> GetMessagesOfDialogAsync(Guid dialogId)
        {
            try
            {
                var messages = await _postgreDbContext.Messages.Where(d => d.DialogId.Equals(dialogId))
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
                                 SenderId = d.SenderId,
                                 SenderName = d.User.Name,
                                 Avatar = d.User.Avatar
                             })
                             .ToListAsync();

                return messages;
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
                if (String.IsNullOrEmpty(dialogId.ToString()))
                {
                    throw new ArgumentNullException($"{dialogId} is null or empty!");
                }

                MessageEntity newMessage = new()
                {
                    MessageId = Guid.NewGuid(),
                    DialogId = dialogId,
                    SenderId = senderId,
                    SendAt = DateTime.Now
                };
                await _postgreDbContext.AddAsync(newMessage);
                await _postgreDbContext.SaveChangesAsync();

                MessageContentsEntity messageContents = new()
                {
                    MessageContentId = Guid.NewGuid(),
                    MessageId = newMessage.MessageId,
                    Content = content,
                    Type = "text"
                };
                await _postgreDbContext.AddAsync(messageContents);

                await _postgreDbContext.SaveChangesAsync();

                var messages = await _postgreDbContext.Messages.Where(d => d.DialogId.Equals(dialogId))
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
                                SenderId = d.SenderId,
                                SenderName = d.User.Name,
                                Avatar = d.User.Avatar
                            })
                            .ToListAsync();

                return messages;
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
                var findedUser = await _postgreDbContext.Users.FirstOrDefaultAsync(u => u.UserId.Equals(userId));

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

                await _postgreDbContext.AddAsync(createdDialog);
                await _postgreDbContext.SaveChangesAsync();
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

                await _postgreDbContext.AddAsync(dialogSender);
                await _postgreDbContext.AddAsync(dialogMember);
                await _postgreDbContext.SaveChangesAsync();
                MessageEntity newMessage = new()
                {
                    MessageId = Guid.NewGuid(),
                    DialogId = createdDialog.DialogId,
                    SenderId = senderId,
                    SendAt = dateTime
                };
                await _postgreDbContext.AddAsync(newMessage);
                await _postgreDbContext.SaveChangesAsync();
                MessageContentsEntity messageContents = new()
                {
                    MessageContentId = Guid.NewGuid(),
                    MessageId = newMessage.MessageId,
                    Content = content,
                    Type = "text"
                };
                await _postgreDbContext.AddAsync(messageContents);

                await _postgreDbContext.SaveChangesAsync();

                var messages = await _postgreDbContext.Messages.Where(d => d.DialogId.Equals(createdDialog.DialogId))
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
                                SenderId = d.SenderId,
                                SenderName = d.User.Name,
                                Avatar = d.User.Avatar
                            })
                            .ToListAsync();

                return messages;
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
                var result = await _postgreDbContext.DialogMembers
                    .Where(u => u.DialogId == dialogId && u.UserId != userId)
                    .Include(u => u.User)
                    .Select(u => new UserEntity
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
                var dialogsToDelete = await _postgreDbContext.Dialogs.Include(d => d.DialogMembers)
                .Where(d => d.DialogMembers.Any(d => d.User.UserId.Equals(userId)))
                .ToListAsync();

                _postgreDbContext.Dialogs.RemoveRange(dialogsToDelete);

                await _postgreDbContext.SaveChangesAsync();

                return true;
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
                //TODO: добавить ещё свойтсва в выходную модель или ОТРЕФАКТОРИТЬ ЭТОТ БЛОК, тут вообще хуйня какая-то если групповые чаты то это не сработает.
                var userDialogs = await _postgreDbContext.DialogMembers
                    .Where(d => d.UserId.Equals(userId))
                    .ToListAsync();

                var otherUserDialog = await _postgreDbContext.DialogMembers
                    .Where(d => d.UserId.Equals(otherUserId))
                    .ToListAsync();

                foreach (var item in userDialogs)
                {
                    var result = otherUserDialog.Select(f => new DialogPropretiesOutput { DialogId = f.DialogId }).FirstOrDefault(f => f.DialogId == item.DialogId);

                    if (result is not null)
                    {
                        return result;
                    }
                }

                //TODO: throw custom exception
                return null;

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
