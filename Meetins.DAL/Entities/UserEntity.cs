using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.Entities
{
    [Table("Users", Schema = "User")]
    public class UserEntity
    {
        [Key]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("Email")]
        public string Email { get; set; }
        //пароль
        [Column("Password")]
        public string Password { get; set; }

        /// <summary>
        /// M - мужчина, F - женщина.
        /// </summary>
        [Column("Gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Путь к иконке пользователя.
        /// </summary>
        [Column("UserIcon")]
        public string UserIcon { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        [Column("DateRegister")]
        public DateTime DateRegister { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Логин и url для пользователя
        /// </summary>
        [Column("LoginUrl")]
        public string LoginUrl { get; set; }
        
        /// <summary>
        /// Запомнить меня
        /// </summary>
        [Column("RememberMe")]
        public bool RememberMe { get; set; }
    }
}
