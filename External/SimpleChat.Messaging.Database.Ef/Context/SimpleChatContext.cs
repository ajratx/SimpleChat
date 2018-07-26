namespace SimpleChat.Messaging.Database.Context
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Base;
    using SimpleChat.Messaging.Database.Ef.EntityTypeConfiguration;
    using SimpleChat.Messaging.Entities;

    public abstract class SimpleChatContext : DbContext
    {
        protected readonly IDatabaseSettings DatabaseSettings;

        protected SimpleChatContext(IDatabaseSettings databaseSettings)
        {
            CheckSettings(databaseSettings);
            DatabaseSettings = databaseSettings;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
        }

        private static void CheckSettings(IDatabaseSettings databaseSettings)
        {
            if (databaseSettings == null)
                throw new ArgumentNullException(nameof(databaseSettings));

            if (string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
                throw new ArgumentException(
                    "Parameter can't be null or white space", 
                    nameof(databaseSettings.ConnectionString));
        }
    }
}
