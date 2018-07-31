namespace SimpleChat.Messaging.Database.Ef.Sqlite.Repository
{
    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;
    using SimpleChat.Messaging.Entities;

    public sealed class UserSqliteRepository : SqliteSimpleChatRepository<User, int>, IUserRepository
    {
        public UserSqliteRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public User GetByEmail(string email)
        {
            CheckDisposed();
            return SimpleChatContext.Set<User>().Find(email);
        }
    }
}
