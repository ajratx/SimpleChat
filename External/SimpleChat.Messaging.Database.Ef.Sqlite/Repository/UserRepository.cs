namespace SimpleChat.Messaging.Database.Ef.Repository
{
    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;
    using SimpleChat.Messaging.Database.Sqlite.Ef.Context;
    using SimpleChat.Messaging.Entities;

    public sealed class UserRepository : SimpleChatRepository<User, int>, IUserRepository
    {
        public UserRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public User GetByEmail(string email)
        {
            CheckDisposed();
            return SimpleChatContext.Set<User>().Find(email);
        }

        protected override SimpleChatContext CreateSimpleChatContext()
            => new SqliteSimpleChatContext(DatabaseSettings);
    }
}
