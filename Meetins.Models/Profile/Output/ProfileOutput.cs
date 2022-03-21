using System;

namespace Meetins.Models.Profile.Output
{
    /// <summary>
    /// Класс выходной модели профиля.
    /// </summary>
    public class ProfileOutput
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }        

        /// <summary>
        /// Статус.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Почта.
        /// </summary>
        public string Email { get; set; }
               
        /// <summary>
        /// Пол.
        /// </summary>
        public string Gender { get; set; }
          
        /// <summary>
        /// Путь к аватару.
        /// </summary>
        public string Avatar { get; set; }
          
        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public DateTime DateRegister { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; }
    }
}
