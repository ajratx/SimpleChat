namespace SimpleChat.Infrastructure
{
    using SimpleChat.Messaging.Base;

    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get ; set; }
    }
}
