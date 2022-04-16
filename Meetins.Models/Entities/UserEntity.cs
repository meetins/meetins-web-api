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
        [Column("DateRegister", TypeName = "timestamp")]
        public DateTime DateRegister { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column("BirthDate", TypeName = "timestamp")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Column("Login", TypeName = "varchar")]
        public string Login { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [Column("Status", TypeName = "varchar")]
        public string Status { get; set; }

        /// <summary>
        /// Идентификатор города.
        /// </summary>
        [Column("CityId", TypeName = "uuid")]
        public Guid CityId { get; set; }

        /// <summary>
        /// Нормализованная почта.
        /// </summary>
        [Column("NormalizedEmail", TypeName = "varchar")]
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// Нормализованный логин.
        /// </summary>
        [Column("NormalizedLogin", TypeName = "varchar")]
        public string NormalizedLogin { get; set; }

        /// <summary>
        /// Хэш пароля.
        /// </summary>
        [Column("PasswordHash", TypeName = "text")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Подтверждён ли емейл.
        /// </summary>
        [Column("EmailConfirmed", TypeName = "boolean")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Код подтверждения емейла.
        /// </summary>
        [Column("ConfirmEmailCode", TypeName = "varchar")]
        public string ConfirmEmailCode { get; set; }

        /// <summary>
        /// Счётчик ошибочных вводов пароля.
        /// </summary>
        [Column("AccessFailedCount", TypeName = "int4")]
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Конец блокировки пользователя.
        /// </summary>
        [Column("LockoutEnd", TypeName = "timestamp")]
        public DateTime? LockoutEnd { get; set; }

        /// <summary>
        /// Заблокирован ли пользователь.
        /// </summary>
        [Column("LockoutEnabled", TypeName = "boolean")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Навигационное свойство для Города.
        /// </summary>
        public CityEntity City { get; set; }

        public ICollection<MessageEntity> Messages { get; set; }
    }
}
