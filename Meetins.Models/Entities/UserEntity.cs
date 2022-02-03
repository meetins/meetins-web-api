using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetins.Models.Entities
{
    [Table("Users", Schema = "User")]
    public class UserEntity
    {
        [Key]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("Name")]
        public string Name { get; set; }        

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
        [Column("Avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        [Column("DateRegister")]
        public DateTime DateRegister { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column("BirthDate")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Column("LoginUrl")]
        public string Login { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("Status")]
        public string Status { get; set; }
        
    }
}
