namespace SimpleChat.Messaging.Base
{
    using System;

    using SimpleChat.Messaging.Entities;

    public interface IUserRepository : IRepository<User, int>
    {
        User GetByEmail(string email);
    }
}
