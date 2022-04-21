using Meetins.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetins.Core.Data
{
    /// <summary>
    /// Класс контекста PostgreSql
    /// </summary>
    public class PostgreDbContext : DbContext
    {
        private readonly DbContextOptions<PostgreDbContext> _options;

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
        {
            _options = options;
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

        /// <summary>
        /// Соответствует таблице dbo.Logs.
        /// </summary>
        public DbSet<LogEntity> Logs { get; set; }

        /// <summary>
        /// Соответствует таблице Events.KudagoInvites.
        /// </summary>
        public DbSet<KudagoInvites> KudagoInvites { get; set; }
    }
}
