namespace SimpleChat.Messaging.Entities
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
