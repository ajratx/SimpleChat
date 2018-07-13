namespace SimpleChat.Messaging.Database.Sqlite
{
    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Database.Interfaces;

    public sealed class SqliteMessagingContext : MessagingContext
    {
        public SqliteMessagingContext(IDatabaseSettings databaseSettings) 
            : base(databaseSettings)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"FileName={DatabaseSettings.ConnectionString}");
        }
    }
}
