﻿using Meetins.Models.Entities;
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
                    Name="Пётр",                    
                    Status = "Зарегистрировался на meetins.ru, чтобы знакомиться и встречаться на интересных событиях!",
                    PhoneNumber = "+375299998877",
                    Email = "petrov@gmail.com",
                    Password = "hash_password_petr",
                    Gender = "M",
                    Avatar = "/images/no-photo.png",
                    DateRegister = new DateTime(2021,03,14),
                    Login = "id_test_user_1"
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
                    Login = "id_test_user_2"                    
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
                    Login = "id_test_user_3"                    
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
                    Login = "id_test_user_4"                    
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