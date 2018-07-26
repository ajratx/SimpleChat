namespace SimpleChat.Messaging.Entities
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
