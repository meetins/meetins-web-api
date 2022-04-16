using Meetins.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace Meetins.Core.Data
{
    public class InMemoryContext : DbContext
    {
        public InMemoryContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseInMemoryDatabase(databaseName: "MeetinsDbInMemory");
        }

        /// <summary>
        /// Соответствует таблице Info.About.
        /// </summary>
        public DbSet<AboutEntity> Abouts { get; set; }

        /// <summary>
        /// Соответствует таблице User.Users.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Соответствует таблице User.RefreshTokens.
        /// </summary>
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.Dialogs.
        /// </summary>
        public DbSet<DialogEntity> Dialogs { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.Messages.
        /// </summary>
        public DbSet<MessageEntity> Messages { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.MessageContents.
        /// </summary>
        public DbSet<MessageContentsEntity> MessageContents { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.DialogMembers.
        /// </summary>
        public DbSet<DialogMembersEntity> DialogMembers { get; set; }

        /// <summary>
        /// Соответствует таблице Events.EventsCategories.
        /// </summary>
        public DbSet<EventsCategoryEntity> EventsCategories { get; set; }

        /// <summary>
        /// Соответствует таблице Events.Events.
        /// </summary>
        public DbSet<EventEntity> Events { get; set; }

        /// <summary>
        /// Соответствует таблице Events.EventsToUsers.
        /// </summary>
        public DbSet<EventsToUsersEntity> EventsToUsers { get; set; }

        /// <summary>
        /// Соответствует таблице dbo.Cities.
        /// </summary>
        public DbSet<CityEntity> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity[]
                {
                new UserEntity {
                    UserId=new Guid("AC6773D0-17C7-4E94-BBDB-649CD88780C8"),
                    Name="Пётр",
                    Status = "Зарегистрировался на meetins.ru, чтобы знакомиться и встречаться на интересных событиях!",
                    PhoneNumber = "+375299998877",
                    Email = "petrov@gmail.com",
                    Password = "11",
                    Gender = "M",
                    Avatar = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,03,14),
                    Login = "id_test_user_1",
                    BirthDate = new DateTime(2000,01,01),
                    CityId = Guid.Parse("187AC176-CB22-4216-9AB5-D3A1EF123456")
                },
                new UserEntity {
                    UserId=new Guid("187AC176-CB28-4456-9AB5-D3A1EF370542"),
                    Name="Жанна",
                    Status = "Зарегистрировалась на meetins.ru, чтобы знакомиться и встречаться на интересных событиях!",
                    PhoneNumber = "+375291112233",
                    Email = "janna@gmail.com",
                    Password = "janna_hash",
                    Gender = "F",
                    Avatar = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,11,12),
                    Login = "id_test_user_2",
                    BirthDate = new DateTime(2000,01,01),
                    CityId = Guid.Parse("187AC345-CB22-4216-9AB5-D3A1EF370542")
                },
                new UserEntity {
                    UserId=new Guid("5BB1C998-E1DA-4E0D-88F8-E1EA1CE2C251"),
                    Name="Дмитрий",
                    PhoneNumber = "+79998887766",
                    Status = "Зарегистрировалась на meetins.ru, чтобы знакомиться и встречаться на интересных событиях!",
                    Email = "DmitriyA7@gmail.com",
                    Password = "adminadmin7",
                    Gender = "M",
                    Avatar = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,12,19),
                    Login = "id_test_user_3",
                    BirthDate = new DateTime(2000,01,01),
                    CityId = Guid.Parse("187AC176-CB22-4216-9AB5-D3A1EF123456")
                },
                new UserEntity {
                    UserId=new Guid("15847892-12EB-4382-9905-FA5F097E60B0"),
                    Name="FirstName",
                    Status = "Зарегистрировалась на meetins.ru, чтобы знакомиться и встречаться на интересных событиях!",
                    PhoneNumber = "+78881112233",
                    Email = "Meettest@gmail.com",
                    Password = "meettest",
                    Gender = "F",
                    Avatar = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,12,19),
                    Login = "id_test_user_4",
                    BirthDate = new DateTime(2000,01,01),
                    CityId = Guid.Parse("187AC176-CB22-4216-9AB5-D3A1EF370542")
                },
                new UserEntity {
                    UserId=new Guid("22247892-12EB-4382-4598-FA5F097E60B0"),
                    Name="Саша",
                    Status = @"\_(O_O)_/",
                    PhoneNumber = "+375298999956",
                    Email = "bigdick666@gmail.com",
                    Password = "1",
                    Gender = "M",
                    Avatar = "/images/no-photo.png",
                    DateRegister = new DateTime(2022,03,21),
                    Login = "sashok",
                    BirthDate = new DateTime(1995,07,17),
                    CityId = Guid.Parse("187AC176-CB22-4216-9AB5-D3A1EF123456")
                },
                });;

            modelBuilder.Entity<AboutEntity>().HasData(
                new AboutEntity[]
                {
                    new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF370542"),
                        MainText = "События",
                        Description = "Посещайте любые события сообща. Знакомьтесь ради дружбы, общения или любви."
                    },
                   new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF372242"),
                        MainText = "Интересы",
                        Description = "Укрепите свои интересы новыми людьми. Узнайте больше и расширяйтесь."
                    },
                   new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF373342"),
                        MainText = "Встречи",
                        Description = "Отправляйте приглашения, встречайтесь в местах развлечений, отдыха и других, дешевле, чем обычно."
                    },
                   new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF376842"),
                        MainText = "Места",
                        Description = "Просматривайте места, которые посещаете. Общайтесь и делитесь впечатлениями от посещения, находите новых друзей где угодно."
                    },

                });

            modelBuilder.Entity<CityEntity>().HasData(
                new CityEntity[]
                {
                    new CityEntity
                    {
                        CityId = new Guid("187AC176-CB22-4216-9AB5-D3A1EF370542"),
                        CityName = "Минск"                        
                    },
                    new CityEntity
                    {
                        CityId = new Guid("187AC345-CB22-4216-9AB5-D3A1EF370542"),
                        CityName = "Москва"
                    },
                    new CityEntity
                    {
                        CityId = new Guid("187AC176-CB22-4216-9AB5-D3A1EF123456"),
                        CityName = "Санкт-Петербург"
                    }
                });
        }
    }
}