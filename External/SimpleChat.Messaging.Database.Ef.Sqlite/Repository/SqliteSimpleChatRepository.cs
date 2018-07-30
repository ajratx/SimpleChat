namespace SimpleChat.Messaging.Database.Ef.Sqlite.Repository
{
    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;
    using SimpleChat.Messaging.Database.Ef.Repository;

    public sealed class SqliteSimpleChatRepository<TEntity, TKey> : SimpleChatRepository<TEntity, TKey>
        where TEntity : class
    {
        public SqliteSimpleChatRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings)
        {
        }

        protected override DbContextOptions<SimpleChatContext> GetDbContextOptions()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<SimpleChatContext>();
            dbContextOptionsBuilder.UseSqlite(DatabaseSettings.ConnectionString);

            return dbContextOptionsBuilder.Options;
        }
    }
}
