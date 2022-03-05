using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Communication.Abstractions
{
    public interface IClients
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Notify(string dialogId, string senderId, string senderName, string senderAvatar, string message);
       
    }
}
