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
    /// В контроллере содержится функционал для получения всех диалогов авторизованного пользователя и получения всех сообщений, принадлежащих диалогу.
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

            //string rawUserId = "187ac176-cb28-4456-9ab5-d3a1ef370500";
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
        [Authorize]
        [HttpPost, Route("messages")]
        public async Task<ActionResult<IEnumerable<MessagesOutput>>> GetMessagesOfDialog([FromBody] Guid dialogId)
        {
            var messages = await _dialogsService.GetMessagesOfDialog(dialogId);

            string rawUserId = HttpContext.User.FindFirst("userId").Value;

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

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

            if (userConnections is not null)
            {
                foreach (var connectionId in userConnections.Connections)
                {
                    await _hubContext.Clients.Client(connectionId.ConnectionId).Notify(message.DialogId.ToString(), userId.ToString(), sender.Name, sender.Avatar, message.Content);
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

            if (userConnections is not null)
            {
                foreach (var connectionId in userConnections.Connections)
                {
                    await _hubContext.Clients.Client(connectionId.ConnectionId).Notify(message.DialogId.ToString(), userId.ToString(), sender.Name, sender.Avatar, message.Content);
                }

            }
            #endregion

            return Ok(messages);
        }
    }
}
