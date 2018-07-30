namespace SimpleChat.Messaging.Database.Ef.Repository
{
    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;
    using SimpleChat.Messaging.Entities;

    public sealed class MessageRepository : SimpleChatRepository<Message, int>, IMessageRepository
    {
        public MessageRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
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
