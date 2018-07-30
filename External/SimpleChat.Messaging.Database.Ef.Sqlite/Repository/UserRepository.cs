namespace SimpleChat.Messaging.Database.Ef.Repository
{
    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Context;
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
    }
}
