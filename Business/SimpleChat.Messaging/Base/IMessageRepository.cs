namespace SimpleChat.Messaging.Base
{
    using SimpleChat.Messaging.Entities;

    public interface IMessageRepository : IRepository<Message, int>
    {
    }
}
