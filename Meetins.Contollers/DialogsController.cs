﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Meetins.Communication;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Meetins.Abstractions.Services;
using Meetins.Models.Messages;
using Meetins.Models.Dialogs.Input;

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

        public DialogsController(IDialogsService dialogsService)
        {
            _dialogsService = dialogsService;
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

            return Ok(messages);
        }

        [Authorize]
        [HttpPost,Route("send-message")]
        public async Task<ActionResult<IEnumerable<MessagesOutput>>> SendMessageAsync([FromBody] MessageInput message)
        {
            string rawUserId = HttpContext.User.FindFirst("userId").Value;
            
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            var messages = await _dialogsService.SendMessageAsync(message.DialogId, userId, message.Content);

            return Ok(messages);
        }
    }
}