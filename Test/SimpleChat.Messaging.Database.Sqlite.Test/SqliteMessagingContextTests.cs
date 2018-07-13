namespace SimpleChat.Messaging.Database.Sqlite.Test
{
    using System;
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Moq;

    using SimpleChat.Messaging.Database.Interfaces;
    using SimpleChat.Messaging.Entities;

    using Xunit;

    public class SqliteMessagingContextTests : IDisposable
    {
        private readonly string databasePath;

        private IDatabaseSettings databaseSettings;

        private SqliteMessagingContext messagingContext;

        public SqliteMessagingContextTests()
        {
            databasePath = Path.Combine(Environment.CurrentDirectory, "MessagingDB.db3");

            databaseSettings = Mock.Of<IDatabaseSettings>();
            databaseSettings.ConnectionString = databasePath;

            if (File.Exists(databasePath)) File.Delete(databasePath);
        }

        public void Dispose() => messagingContext?.Dispose();

        [Fact]
        public void Constructor_NullSettings_ExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => messagingContext = new SqliteMessagingContext(null));
        }

        [Fact]
        public void Constructor_ConnenctionStringIsNull_ExceptionThrown()
        {
            databaseSettings.ConnectionString = null;
            Assert.Throws<ArgumentException>(() => messagingContext = new SqliteMessagingContext(databaseSettings));
        }

        [Fact]
        public void Constructor_ConnenctionStringIsEmpty_ExceptionThrown()
        {
            databaseSettings.ConnectionString = string.Empty;
            Assert.Throws<ArgumentException>(() => messagingContext = new SqliteMessagingContext(databaseSettings));
        }

        [Fact]
        public void Constructor_ConnenctionStringIsWhiteSpace_ExceptionThrown()
        {
            databaseSettings.ConnectionString = " ";
            Assert.Throws<ArgumentException>(() => messagingContext = new SqliteMessagingContext(databaseSettings));
        }

        [Fact]
        public void AddGetUpdateGetDeleteGetUser()
        {
            var userName = "admin";

            messagingContext = new SqliteMessagingContext(databaseSettings);
            messagingContext.Database.EnsureCreated();

            var user = new User { Name = userName, Online = true };
            AddItem(user);

            var userFromDb = messagingContext.Users.Find(1);
            Assert.NotNull(userFromDb);
            Assert.Equal(userName, userFromDb.Name);
            Assert.True(userFromDb.Online);

            userFromDb.Online = false;
            UpdateItem(userFromDb);

            userFromDb = messagingContext.Users.Find(1);
            Assert.NotNull(userFromDb);
            Assert.Equal(userName, userFromDb.Name);
            Assert.False(userFromDb.Online);

            DeleteItem(userFromDb);

            userFromDb = messagingContext.Users.Find(1);
            Assert.Null(userFromDb);
        }

        [Fact]
        public void AddGetUpdateGetDeleteGetMessage()
        {
            messagingContext = new SqliteMessagingContext(databaseSettings);
            messagingContext.Database.EnsureCreated();

            var user = new User { Name = "admin", Online = true };
            AddItem(user);

            var message = new Message { UserId = 1, Text = "Message text" };
            AddItem(message);

            var messageFromDb = messagingContext.Messages.Find(1);
            Assert.NotNull(messageFromDb);
            Assert.Equal("Message text", messageFromDb.Text);
            Assert.Equal(1, messageFromDb.UserId);

            messageFromDb.Text = "Another message text";
            UpdateItem(messageFromDb);

            messageFromDb = messagingContext.Messages.Find(1);
            Assert.NotNull(messageFromDb);
            Assert.Equal("Another message text", messageFromDb.Text);
            Assert.Equal(1, messageFromDb.UserId);

            DeleteItem(messageFromDb);

            messageFromDb = messagingContext.Messages.Find(1);
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
            messagingContext.Attach(item);
            messagingContext.Entry(item).State = entityState;
            messagingContext.SaveChanges();
        }
    }
}
