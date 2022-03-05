using Meetins.Models.Messages;
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
        Task Notify(MessagesOutput message);
       
    }
}
