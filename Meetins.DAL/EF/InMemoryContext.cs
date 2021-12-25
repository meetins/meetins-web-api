using Meetins.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetins.DAL.EF
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
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=meetins;Trusted_Connection=True;");
            optionsBuilder.UseInMemoryDatabase(databaseName: "MeetinsDbInMemory");
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<AboutEntity> Abouts { get; set; }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity[]
                {
                new UserEntity { 
                    UserId=new Guid("AC6773D0-17C7-4E94-BBDB-649CD88780C8"), 
                    FirstName="Пётр",
                    LastName = "Петров",
                    PhoneNumber = "+375299998877",
                    Email = "petrov@gmail.com",
                    Password = "hash_password_petr",
                    Gender = "M",
                    UserIcon = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,03,14),
                    LoginUrl = "id_test_user#1",
                    RememberMe = true
                },
                new UserEntity { 
                    UserId=new Guid("187AC176-CB28-4456-9AB5-D3A1EF370542"),
                    FirstName="Жанна",
                    LastName = "Агузарова",
                    PhoneNumber = "+375291112233",
                    Email = "janna@gmail.com",
                    Password = "janna_hash",
                    Gender = "F",
                    UserIcon = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,11,12),
                    LoginUrl = "id_test_user#2",
                    RememberMe = false
                },
                new UserEntity {
                    UserId=new Guid("5BB1C998-E1DA-4E0D-88F8-E1EA1CE2C251"),
                    FirstName="Дмитрий",
                    LastName = "А7",
                    PhoneNumber = "+79998887766",
                    Email = "DmitriyA7@gmail.com",
                    Password = "adminadmin7",
                    Gender = "M",
                    UserIcon = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,12,19),
                    LoginUrl = "id_test_user#3",
                    RememberMe = false
                },
                new UserEntity {
                    UserId=new Guid("15847892-12EB-4382-9905-FA5F097E60B0"),
                    FirstName="FirstName",
                    LastName = "LastName",
                    PhoneNumber = "+78881112233",
                    Email = "Meettest@gmail.com",
                    Password = "meettest",
                    Gender = "F",
                    UserIcon = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,12,19),
                    LoginUrl = "id_test_user#4",
                    RememberMe = false
                },
                });

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
        }
    }
}
