namespace SimpleChat.Messaging.Database.Sqlite.Test
{
    using System;
    using System.IO;

    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Database.Context;
    using SimpleChat.Messaging.Entities;

    using Xunit;

    public class SimpleChatContextTests : IDisposable
    {
        private readonly DbContextOptionsBuilder<SimpleChatContext> dbContextOptionsBuilder;

        private SimpleChatContext simpleChatContext;

        public SimpleChatContextTests()
        {
            var databaseFilePath = Path.Combine(Environment.CurrentDirectory, "MessagingDB.db3");
            dbContextOptionsBuilder = new DbContextOptionsBuilder<SimpleChatContext>();
            dbContextOptionsBuilder.UseSqlite($"Filename={databaseFilePath}");

            if (File.Exists(databaseFilePath)) File.Delete(databaseFilePath);
        }

        public void Dispose() => simpleChatContext?.Dispose();

        [Fact]
        public void Constructor_NullSettings_ExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => simpleChatContext = new SimpleChatContext(null));
        }

        [Fact]
        public void AddGetUpdateGetDeleteGetUser()
        {
            var userName = "admin";

            simpleChatContext = new SimpleChatContext(dbContextOptionsBuilder.Options);
            simpleChatContext.Database.EnsureCreated();

            var user = new User { Name = userName, Email = "admin@mail.org" };
            AddItem(user);

            var userFromDb = simpleChatContext.Users.Find(1);
            Assert.NotNull(userFromDb);
            Assert.Equal(userName, userFromDb.Name);
        
            UpdateItem(userFromDb);

            userFromDb = simpleChatContext.Users.Find(1);
            Assert.NotNull(userFromDb);
            Assert.Equal(userName, userFromDb.Name);

            DeleteItem(userFromDb);

            userFromDb = simpleChatContext.Users.Find(1);
            Assert.Null(userFromDb);
        }

        [Fact]
        public void AddGetUpdateGetDeleteGetMessage()
        {
            simpleChatContext = new SimpleChatContext(dbContextOptionsBuilder.Options);
            simpleChatContext.Database.EnsureCreated();

            var user = new User { Name = "admin", Email = "admin@mail.org" };
            AddItem(user);

            var message = new Message { UserId = 1, Text = "Message text" };
            AddItem(message);

            var messageFromDb = simpleChatContext.Messages.Find(1);
            Assert.NotNull(messageFromDb);
            Assert.Equal("Message text", messageFromDb.Text);
            Assert.Equal(1, messageFromDb.UserId);

            messageFromDb.Text = "Another message text";
            UpdateItem(messageFromDb);

            messageFromDb = simpleChatContext.Messages.Find(1);
            Assert.NotNull(messageFromDb);
            Assert.Equal("Another message text", messageFromDb.Text);
            Assert.Equal(1, messageFromDb.UserId);

            DeleteItem(messageFromDb);

            messageFromDb = simpleChatContext.Messages.Find(1);
            Assert.Null(messageFromDb);
        }

        private void AddItem<T>(T item) where T : class 
            => AttachItemAndSaveChanges(item, EntityState.Added);

        private void UpdateItem<T>(T item) where T : class 
            => AttachItemAndSaveChanges(item, EntityState.Modified);

        private void DeleteItem<T>(T item) where T : class 
            => AttachItemAndSaveChanges(item, EntityState.Deleted);

        private void AttachItemAndSaveChanges<T>(T item, EntityState entityState)
            where T : class
        {
            simpleChatContext.Entry(item).State = entityState;
            simpleChatContext.SaveChanges();
        }
    }
}
