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

        public DbSet<AboutEntity> Abouts { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DbSet<DialogEntity> Dialogs { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<AboutMessageContent> MessageContents { get; set; }

        public DbSet<AboutMessageToUser> MessageToUser { get; set; }

        public DbSet<DialogMembers> DialogMembers { get; set; }
    }
}
