using System;

namespace Meetins.Models.People
{
    /// <summary>
    /// Выходная модель для списка пользователей.
    /// </summary>
    public class PeopleOutput
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Аватар пользователя.
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; }
    }
}
