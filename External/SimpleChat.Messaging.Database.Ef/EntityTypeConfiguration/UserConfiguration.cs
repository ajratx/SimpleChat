namespace SimpleChat.Messaging.Database.Ef.EntityTypeConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SimpleChat.Messaging.Entities;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasAlternateKey(user => user.Email);
            builder.Property(user => user.Name).IsRequired().HasMaxLength(255);
        }
    }
}
