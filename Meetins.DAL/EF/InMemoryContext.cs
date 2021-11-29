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
                    UserIcon = "path_to_petr_avatar.png",
                    DateRegister = new DateTime(2021,03,14),
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
                    UserIcon = "path_to_janna_avatar.png",
                    DateRegister = new DateTime(2021,11,12),
                    RememberMe = false
                }
                });

            modelBuilder.Entity<AboutEntity>().HasData(
                new AboutEntity[]
                {
                    new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF370542"),
                        MainText = "События",
                        Description = ""
                    },
                   new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF372242"),
                        MainText = "Интересы",
                        Description = ""
                    },
                   new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF373342"),
                        MainText = "Встречи",
                        Description = ""
                    },
                   new AboutEntity
                    {
                        AboutId = new Guid("187AC176-CB28-4216-9AB5-D3A1EF376842"),
                        MainText = "Места",
                        Description = ""
                    },

                });
        }
    }
}
