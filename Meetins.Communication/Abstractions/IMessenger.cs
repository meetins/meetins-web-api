using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetins.Communication.Abstractions
{
    public interface IMessenger
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(string userName, string message);

        /// <summary>
        /// Update user list
        /// </summary>
        /// <param name="users"></param>
        Task UpdateUsersAsync(IEnumerable<string> users);

        Task ReceiveBroadcast(string message);
    }
}
