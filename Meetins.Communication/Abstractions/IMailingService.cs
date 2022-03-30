using System.Threading.Tasks;

namespace Meetins.Communication.Abstractions
{
    /// <summary>
    /// Абстракция сервиса рассылок.
    /// </summary>
    public interface IMailingService
    {
        /// <summary>
        /// Метод отправит код подтверждения на почту.
        /// </summary>
        /// <param name="code">Код подтверждения.</param>
        /// <param name="emailTo">email на который отправить сообщение.</param>
        Task SendAcceptCodeMailAsync(string code, string emailTo);
    }
}
