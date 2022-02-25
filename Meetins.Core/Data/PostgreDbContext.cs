using Meetins.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetins.Core.Data
{
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext()
        {
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=34.116.153.6;Port=5432;Database=meetins_test_db;Username=meetinsadmin;Password=m33tt3st");
        }

        /// <summary>
        /// Соответствует таблице Info.About
        /// </summary>
        public DbSet<AboutEntity> Abouts { get; set; }

        /// <summary>
        /// Соответствует таблице User.Users
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Соответствует таблице User.RefreshTokens
        /// </summary>
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.Dialogs
        /// </summary>
        public DbSet<DialogEntity> Dialogs { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.Messages
        /// </summary>
        public DbSet<MessageEntity> Messages { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.MessageContents
        /// </summary>
        public DbSet<MessageContentsEntity> MessageContents { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.ChatMessage
        /// </summary>
        public DbSet<ChatMessageEntity> ChatMessage { get; set; }

        /// <summary>
        /// Соответствует таблице Messenger.DialogMembers
        /// </summary>
        public DbSet<DialogMembersEntity> DialogMembers { get; set; }
    }
}
