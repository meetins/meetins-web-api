using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    /// <summary>
    /// Класс сопоставляется с таблицей User.Users.
    /// </summary>
    [Table("Users", Schema = "User")]
    public class UserEntity
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [Key]
        [Column("UserId", TypeName = "uuid")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Column("Name", TypeName = "varchar")]
        public string Name { get; set; }        

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        [Column("PhoneNumber", TypeName = "varchar")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Электронный адрес пользователя.
        /// </summary>
        [Column("Email", TypeName = "varchar")]
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Column("Password", TypeName = "varchar")]
        public string Password { get; set; }

        /// <summary>
        /// M - мужчина, F - женщина.
        /// </summary>
        [Column("Gender", TypeName = "varchar")]
        public string Gender { get; set; }

        /// <summary>
        /// Путь к иконке пользователя.
        /// </summary>
        [Column("Avatar", TypeName = "varchar")]
        public string Avatar { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        [Column("DateRegister", TypeName = "date")]
        public DateTime DateRegister { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column("BirthDate", TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Column("LoginUrl", TypeName = "varchar")]
        public string Login { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [Column("Status", TypeName = "varchar")]
        public string Status { get; set; }
    }
}
