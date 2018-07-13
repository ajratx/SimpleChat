namespace SimpleChat.Messaging.Database
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using SimpleChat.Messaging.Database.Interfaces;
    using SimpleChat.Messaging.Entities;

    public abstract class MessagingContext : DbContext
    {
        protected readonly IDatabaseSettings DatabaseSettings;

        protected MessagingContext(IDatabaseSettings databaseSettings)
        {
            CheckSetting(databaseSettings);
            DatabaseSettings = databaseSettings;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().HasAlternateKey(user => user.Name);
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(user => user.Online).IsRequired();

            modelBuilder.Entity<Message>().HasKey(message => message.Id);
            modelBuilder.Entity<Message>().Property(message => message.UserId).IsRequired();
            modelBuilder.Entity<Message>()
                .HasOne(message => message.User).WithMany(user => user.Messages)
                .HasForeignKey(message => message.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void CheckSetting(IDatabaseSettings dbSettings)
        {
            if (dbSettings == null) throw new ArgumentNullException(nameof(dbSettings));

            if (string.IsNullOrWhiteSpace(dbSettings.ConnectionString))
                throw new ArgumentException("Parameter can't be null or white space", nameof(dbSettings.ConnectionString));
        }
    }
}
