using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Meetins.Communication;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Meetins.Abstractions.Services;
using Meetins.Models.Messages;
using Meetins.Models.Dialogs.Input;
using Microsoft.AspNetCore.SignalR;
using Meetins.Communication.Hubs;
using Meetins.Communication.Abstractions;
using Meetins.Communication.Models;
using System.Linq;
using Meetins.Models.User.Output;
using Meetins.Abstractions.Repositories;

namespace Meetins.Contollers
{
    /// <summary>
    /// В контроллере содержится функционал дял мессенджера.
    /// </summary>
    [Route("dialogs")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        IDialogsService _dialogsService;
        IUserRepository _userRepository;
        private readonly IHubContext<MessengerHub, IClients> _hubContext;
        private readonly MessengerManager _messengerManager;

        public DialogsController(IDialogsService dialogsService, IHubContext<MessengerHub, IClients> hubContext, MessengerManager messengerManager, IUserRepository userRepository)
        {
            _dialogsService = dialogsService;
            _hubContext = hubContext;
            _messengerManager = messengerManager;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Получения всех диалогов авторизованного пользователя.
        /// </summary>
        /// <returns> Диалоги пользователя. </returns>
        [Authorize]
        [HttpGet, Route("my-dialogs")]
        public async Task<ActionResult<IEnumerable<DialogsOutput>>> GetDialogsAsync()
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            //string rawUserId = "5db1031e-ca48-46d1-b9ea-d9e7ebb8c6e6";
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var result = await _dialogsService.GetDialogsAsync(userId);

            return Ok(result);
        }

        /// <summary>
        /// Получение всех сообщений, принадлежащих диалогу.
        /// </summary>
        /// <param name="dialogId"> Идентификатор диалога. </param>
        /// <returns> Сообщения пользователя. </returns>
        //[Authorize]
        [HttpPost, Route("messages")]
        public async Task<ActionResult<IEnumerable<MessagesOutput>>> GetMessagesOfDialog([FromBody] Guid dialogId)
        {
            string rawUserId = "c76e4f1a-d519-4400-b2c4-f95ad7dbc8a0";
            //string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }
            
            var messages = await _dialogsService.GetMessagesOfDialog(dialogId);            

            foreach (var message in messages)
            {
                message.IsMine = userId == message.SenderId;
            }

            return Ok(messages);
        }


        [Authorize]
        [HttpPost, Route("start-dialog")]
        public async Task<ActionResult<IEnumerable<MessagesOutput>>> StartDialogAsync([FromBody] StartDialogInput startDialog)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var messages = await _dialogsService.StartDialogAsync(userId, startDialog.UserId, startDialog.MessageContent);

            foreach (var mess in messages)
            {
                mess.IsMine = userId == mess.SenderId;
            }


            #region notify
            //TODO: вынести в сервис
            //notify
            MessagesOutput message = messages.First();
            var sender = await _userRepository.GetUserByIdAsync(userId);

            //получаем всех участников диалога
            var recipientsAll = await _dialogsService.GetOtherDialogMembersAsync(message.DialogId, userId);

            //отправлять будем одному
            UserOutput recipient = recipientsAll.First();

            //смотрим в менеджере какие юзеры подключены
            var connectedUsers = _messengerManager.Users.ToList();

            foreach (var user in connectedUsers)
            {
                Console.WriteLine($"{user.UserName}, {user.Connections.ToList().First().ConnectionId}, {user.ConnectedAt.Value}");
            }

            //получаем список всех подключений пользователя
            var userConnections = _messengerManager.Users.FirstOrDefault(u => u.UserName.Equals(recipient.UserId.ToString()));

            var mes2 = messages.LastOrDefault();
            MessagesOutput mes = new MessagesOutput()
            {
                MessageId = mes2.MessageId,
                Content = mes2.Content,
                DialogId = mes2.DialogId,
                Avatar = mes2.Avatar,
                IsMine = false,
                IsRead = false,
                SendAt = mes2.SendAt,
                SenderId = mes2.SenderId,
                SenderName = mes2.SenderName
            };

            if (userConnections is not null)
            {
                foreach (var connectionId in userConnections.Connections)
                {
                    await _hubContext.Clients.Client(connectionId.ConnectionId).Notify(mes);
                }

            }
            #endregion

            return Ok(messages);
        }

        [Authorize]
        [HttpPost, Route("send-message")]
        public async Task<ActionResult<IEnumerable<MessagesOutput>>> SendMessageAsync([FromBody] MessageInput message)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var messages = await _dialogsService.SendMessageAsync(message.DialogId, userId, message.Content);

            foreach (var mess in messages)
            {
                mess.IsMine = userId == mess.SenderId;
            }

            #region notify
            //TODO: вынести в сервис
            //notify

            var sender = await _userRepository.GetUserByIdAsync(userId);

            //получаем всех участников диалога
            var recipientsAll = await _dialogsService.GetOtherDialogMembersAsync(message.DialogId, userId);

            //отправлять будем одному
            UserOutput recipient = recipientsAll.First();

            //смотрим в менеджере какие юзеры подключены
            var connectedUsers = _messengerManager.Users.ToList();

            foreach (var user in connectedUsers)
            {
                Console.WriteLine($"{user.UserName}, {user.Connections.ToList().First().ConnectionId}, {user.ConnectedAt.Value}");
            }

            //получаем список всех подключений пользователя
            var userConnections = _messengerManager.Users.FirstOrDefault(u => u.UserName.Equals(recipient.UserId.ToString()));
           
            var mes2 = messages.LastOrDefault();
            MessagesOutput mes = new MessagesOutput()
            {
                MessageId = mes2.MessageId,
                Content = mes2.Content,
                DialogId = mes2.DialogId,
                Avatar = mes2.Avatar,
                IsMine = false,
                IsRead = false,
                SendAt = mes2.SendAt,
                SenderId = mes2.SenderId,
                SenderName = mes2.SenderName
            };      
            

            if (userConnections is not null)
            {
                foreach (var connectionId in userConnections.Connections)
                {
                    await _hubContext.Clients.Client(connectionId.ConnectionId).Notify(mes);
                }

            }
            #endregion

            return Ok(messages);
        }

        /// <summary>
        /// Метод получает сообщения личного диалога с пользователем.
        /// </summary>
        /// <param name="otherUserId">Идентификатор пользователя.</param>
        /// <returns>Сообщения в диалоге.</returns>
        [Authorize]
        [HttpPost]
        [Route("private-dialog")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MessagesOutput>))]
        public async Task<ActionResult<IEnumerable<MessagesOutput>>> GetMessagesOfPrivateDialogAsync([FromBody] Guid otherUserId)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var findedDialog = await _dialogsService.GetPrivateDialogAsync(userId, otherUserId);

            IEnumerable<MessagesOutput> messages = null;

            if (findedDialog is not null)
            {
                messages = await _dialogsService.GetMessagesOfDialog(findedDialog.DialogId);

                foreach (var message in messages)
                {
                    message.IsMine = userId == message.SenderId;
                }
            }

            return Ok(messages);
        }
    }
}
