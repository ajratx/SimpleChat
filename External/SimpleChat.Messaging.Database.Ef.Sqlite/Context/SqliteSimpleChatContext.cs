namespace SimpleChat.Messaging.Database.Sqlite.Ef.Context
{
    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;

    public sealed class SqliteSimpleChatContext : SimpleChatContext
    {
        public SqliteSimpleChatContext(IDatabaseSettings databaseSettings) 
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
