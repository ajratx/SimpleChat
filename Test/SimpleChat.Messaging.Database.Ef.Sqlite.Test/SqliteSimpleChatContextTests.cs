namespace SimpleChat.Messaging.Database.Sqlite.Test
{
    using System;
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Moq;

    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Sqlite.Ef.Context;
    using SimpleChat.Messaging.Entities;

    using Xunit;

    public class SqliteSimpleChatContextTests : IDisposable
    {
        private readonly string databaseFile;

        private IDatabaseSettings databaseSettings;

        private SqliteSimpleChatContext simpleChatContext;

        public SqliteSimpleChatContextTests()
        {
            databaseFile = Path.Combine(Environment.CurrentDirectory, "MessagingDB.db3");

            databaseSettings = Mock.Of<IDatabaseSettings>();
            databaseSettings.ConnectionString = databaseFile;

            if (File.Exists(databaseFile)) File.Delete(databaseFile);
        }

        public void Dispose() => simpleChatContext?.Dispose();

        [Fact]
        public void Constructor_NullSettings_ExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => simpleChatContext = new SqliteSimpleChatContext(null));
        }

        [Fact]
        public void Constructor_ConnenctionStringIsNull_ExceptionThrown()
        {
            databaseSettings.ConnectionString = null;
            Assert.Throws<ArgumentException>(() => simpleChatContext = new SqliteSimpleChatContext(databaseSettings));
        }

        [Fact]
        public void Constructor_ConnenctionStringIsEmpty_ExceptionThrown()
        {
            databaseSettings.ConnectionString = string.Empty;
            Assert.Throws<ArgumentException>(() => simpleChatContext = new SqliteSimpleChatContext(databaseSettings));
        }

        [Fact]
        public void Constructor_ConnenctionStringIsWhiteSpace_ExceptionThrown()
        {
            databaseSettings.ConnectionString = " ";
            Assert.Throws<ArgumentException>(() => simpleChatContext = new SqliteSimpleChatContext(databaseSettings));
        }

        [Fact]
        public void AddGetUpdateGetDeleteGetUser()
        {
            var userName = "admin";

            simpleChatContext = new SqliteSimpleChatContext(databaseSettings);
            simpleChatContext.Database.EnsureCreated();

            var user = new User { Name = userName };
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
            simpleChatContext = new SqliteSimpleChatContext(databaseSettings);
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
            simpleChatContext.Attach(item);
            simpleChatContext.Entry(item).State = entityState;
            simpleChatContext.SaveChanges();
        }
    }
}
